# GAME RULES

Bu dosya, Fate in Your Hands icin baglayici oyun kurallarini tanimlar.

Ana karar: Oynanista tek ana kaynak candir. Baska oynanis puani tutulmaz.

## 1. Temel Kural

Oyuncu her hikayeye 3 canla baslar.

```yaml
player:
  can: 3
```

Bu deger disinda oyuncu icin baska sayisal ilerleme alani tanimlanmaz.

## 2. Kart Akisi

Oyun kartlardan olusur.

Kart turleri:

- `choice`
- `death`
- `ending`
- `titleUnlock`

Her `choice` karti tam olarak 2 secenek sunar.

Secenek sayisi:

- 1 olamaz.
- 3 veya daha fazla olamaz.
- Gizli ucuncu secenek kullanilmaz.

## 3. Secenek Kurali

Her secenek sunlari tasir:

```yaml
choice:
  label: "Lokantaya git"
  canDelta: 0
  nextCardId: ybh_002
```

Zorunlu alanlar:

- `label`
- `canDelta`
- `nextCardId`

`canDelta` yalnizca su degerleri alir:

- `0`
- `-1`
- `1`

Can artisi nadir kullanilir.

## 4. Can Kurali

Can 0 olursa oyuncu olum kartina gider.

```yaml
rule:
  ifCanIsZero: death
```

Olum karti secimin hemen ardindan acilabilir. Bazi secimler can 0 olmadan da dogrudan olum kartina gidebilir.

## 5. Olum Kartlari

Olum karti formati:

```yaml
card:
  cardId: ybh_death_001
  type: death
  title: "Sogukta Uyku"
  text: "Beklemek guvenli sandin. Sabaha kadar hava senden daha guclu cikti."
  unlockTitle: "ilk_olum"
```

Olum kartlari:

- kisa yazilir,
- secimin bedelini hissettirir,
- oyuncuyu tekrar denemeye iter,
- uzun aciklama yapmaz.

## 6. Final Kartlari

Final karti formati:

```yaml
card:
  cardId: ybh_ending_001
  type: ending
  title: "Yeni Sabah"
  text: "Sabaha ulastin. Sehir hala yabanci, ama artik tamamen kaybolmus degilsin."
  unlockTitle: "hayatta_kalan"
```

Her hikaye en az 5 finale sahip olur.

Final metni oyuncunun ulastigi rotayi hissettirmelidir.

## 7. Unvanlar

Unvanlar oynanisi degistirmez.

Unvanlar sadece sunlar icin kullanilir:

- koleksiyon
- galeri
- oyuncuya hedef hissi vermek

Ornek:

```yaml
title:
  titleId: ilk_olum
  displayName: "Ilk Olum"
  description: "Ilk kez bir olum karti gordun."
```

## 8. Kayit Kurali

Kayit dosyasi yalnizca gerekli bilgileri saklar.

```yaml
save:
  schemaVersion: 1
  storyId: yeni_bir_hayat
  currentCardId: ybh_001
  checkpointCardId: ybh_001
  can: 3
  seenCards:
    - ybh_001
  seenDeaths:
    - ybh_death_001
  seenEndings:
    - ybh_ending_001
  unlockedTitles:
    - ilk_olum
```

Kayit dosyasinda can disinda oynanis puani tutulmaz.

## 9. Hikaye Manifesti

Her hikaye tek manifest ile tanimlanir.

```yaml
story:
  storyId: yeni_bir_hayat
  title: "Yeni Bir Hayat"
  startCardId: ybh_001
  startingCan: 3
  endingCount: 5
  deathCount: 10
```

## 10. Ornek Kartlar

```yaml
cards:
  - cardId: ybh_001
    type: choice
    title: "Otogar"
    text: "Gece yarisi otogarda uyandin. Hava soguk. Karsida isigi yanan eski bir lokanta var."
    choices:
      - label: "Lokantaya git"
        canDelta: 0
        nextCardId: ybh_002
      - label: "Durakta kal"
        canDelta: -1
        nextCardId: ybh_003

  - cardId: ybh_002
    type: choice
    title: "Isikli Lokanta"
    text: "Kapidaki zil sen girince titrek bir ses cikarir. Iceride herkes bir an susar."
    choices:
      - label: "Masaya otur"
        canDelta: 0
        nextCardId: ybh_004
      - label: "Geri cik"
        canDelta: -1
        nextCardId: ybh_005

  - cardId: ybh_003
    type: death
    title: "Sogukta Uyku"
    text: "Sabaha kadar beklemeyi secmis olabilirsin. Soguk beklemeyi sevmez."
    unlockTitle: "ilk_olum"
```

## 11. UI Kurallari

Oyun ekraninda yalnizca su bilgiler bulunur:

- can gostergesi
- kart basligi
- kart metni
- iki secim butonu
- menu butonu

Ekranda baska oynanis gostergesi bulunmaz.

## 12. Prototip Kapsami

Ilk oynanabilir surum:

- 1 hikaye
- 45-60 kart
- 3 can
- 2 secenekli karar kartlari
- en az 10 olum karti
- en az 5 final karti
- en az 8 unvan
- son secime don
- galeri

## 13. Kabul Kriterleri

Bir hikaye kabul edilir sayilmak icin:

- tum karar kartlari 2 secenekli olmalidir,
- her secenek `canDelta` icermelidir,
- can 0 oldugunda olum karti acilmalidir,
- en az 5 final bulunmalidir,
- can disinda oynanis puani bulunmamalidir,
- kayit dosyasi yalnizca can ve ilerleme bilgilerini saklamalidir.
