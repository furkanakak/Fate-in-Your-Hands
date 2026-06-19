param(
  [Parameter(Mandatory = $true)]
  [string]$QueuePath,

  [int]$StartIndex = 1,
  [int]$EndIndex = 2147483647,

  [string]$SessionsRoot = "$env:USERPROFILE\.codex\sessions",

  [switch]$Overwrite,

  [switch]$Quiet
)

$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Drawing

function Read-QueueRecords {
  param([string]$Path)

  Get-Content -LiteralPath $Path -Encoding UTF8 | ForEach-Object {
    if (-not [string]::IsNullOrWhiteSpace($_)) {
      $_ | ConvertFrom-Json
    }
  } | Where-Object {
    $_.index -ge $StartIndex -and $_.index -le $EndIndex
  }
}

function Get-AssetIdFromPrompt {
  param([string]$Prompt)

  if ($Prompt -match 'AssetId:\s*([A-Za-z0-9_]+)') {
    return $Matches[1].Trim()
  }
  return $null
}

function Get-CornerFillColor {
  param([System.Drawing.Bitmap]$Image)

  $w = $Image.Width
  $h = $Image.Height
  $samples = @(
    $Image.GetPixel(5, 5),
    $Image.GetPixel([Math]::Max(0, $w - 6), 5),
    $Image.GetPixel(5, [Math]::Max(0, $h - 6)),
    $Image.GetPixel([Math]::Max(0, $w - 6), [Math]::Max(0, $h - 6))
  )

  $r = [int](($samples | ForEach-Object { $_.R } | Measure-Object -Average).Average)
  $g = [int](($samples | ForEach-Object { $_.G } | Measure-Object -Average).Average)
  $b = [int](($samples | ForEach-Object { $_.B } | Measure-Object -Average).Average)
  [System.Drawing.Color]::FromArgb(255, $r, $g, $b)
}

function Save-NormalizedPng {
  param(
    [byte[]]$Bytes,
    [string]$TargetPath,
    [int]$TargetWidth,
    [int]$TargetHeight
  )

  $targetDir = Split-Path -Parent $TargetPath
  New-Item -ItemType Directory -Force -Path $targetDir | Out-Null

  $memory = New-Object System.IO.MemoryStream(,$Bytes)
  $source = [System.Drawing.Bitmap]::FromStream($memory)

  try {
    $canvas = New-Object System.Drawing.Bitmap $TargetWidth, $TargetHeight
    $graphics = [System.Drawing.Graphics]::FromImage($canvas)
    try {
      $graphics.Clear((Get-CornerFillColor -Image $source))
      $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
      $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
      $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality

      $scale = [Math]::Min($TargetWidth / $source.Width, $TargetHeight / $source.Height)
      $drawWidth = [int][Math]::Round($source.Width * $scale)
      $drawHeight = [int][Math]::Round($source.Height * $scale)
      $x = [int][Math]::Floor(($TargetWidth - $drawWidth) / 2)
      $y = [int][Math]::Floor(($TargetHeight - $drawHeight) / 2)

      $dest = New-Object System.Drawing.Rectangle $x, $y, $drawWidth, $drawHeight
      $graphics.DrawImage($source, $dest)
    }
    finally {
      $graphics.Dispose()
    }

    $tmp = "$TargetPath.tmp.png"
    $canvas.Save($tmp, [System.Drawing.Imaging.ImageFormat]::Png)
    $canvas.Dispose()
    Move-Item -LiteralPath $tmp -Destination $TargetPath -Force
  }
  finally {
    $source.Dispose()
    $memory.Dispose()
  }
}

$records = @(Read-QueueRecords -Path $QueuePath)
$wanted = @{}
foreach ($record in $records) {
  $wanted[$record.assetId] = $record
}

$latestByAsset = @{}
$sessionFiles = Get-ChildItem -LiteralPath $SessionsRoot -Recurse -Filter *.jsonl |
  Sort-Object LastWriteTimeUtc

foreach ($file in $sessionFiles) {
  Get-Content -LiteralPath $file.FullName -Encoding UTF8 | ForEach-Object {
    try {
      $event = $_ | ConvertFrom-Json
    }
    catch {
      return
    }

    $payload = $event.payload
    if ($null -eq $payload) {
      return
    }

    if ($payload.type -ne "image_generation_end") {
      return
    }

    $assetId = Get-AssetIdFromPrompt -Prompt $payload.revised_prompt
    if ($null -eq $assetId -or -not $wanted.ContainsKey($assetId)) {
      return
    }

    if ([string]::IsNullOrWhiteSpace($payload.result)) {
      return
    }

    $latestByAsset[$assetId] = $payload.result
  }
}

$written = 0
$skipped = 0
$missing = 0

foreach ($record in $records) {
  $target = [string]$record.targetPathAbs

  if ((Test-Path -LiteralPath $target) -and -not $Overwrite) {
    $skipped += 1
    continue
  }

  if (-not $latestByAsset.ContainsKey($record.assetId)) {
    if (-not $Quiet) {
      Write-Output "MISSING`t$($record.index)`t$($record.assetId)`t$target"
    }
    $missing += 1
    continue
  }

  $bytes = [Convert]::FromBase64String($latestByAsset[$record.assetId])
  Save-NormalizedPng -Bytes $bytes -TargetPath $target -TargetWidth ([int]$record.width) -TargetHeight ([int]$record.height)
  if (-not $Quiet) {
    Write-Output "WROTE`t$($record.index)`t$($record.assetId)`t$target"
  }
  $written += 1
}

Write-Output "SUMMARY`tselected=$($records.Count)`twritten=$written`tskipped=$skipped`tmissing=$missing"
