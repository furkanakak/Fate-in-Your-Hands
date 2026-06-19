using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FateInYourHands.Story
{
    public sealed class SmmStoryImageLoader
    {
        private readonly Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
        private readonly SmmStoryRepository repository;

        public SmmStoryImageLoader(SmmStoryRepository repository)
        {
            this.repository = repository;
        }

        public Sprite LoadSprite(string assetId)
        {
            if (string.IsNullOrWhiteSpace(assetId))
            {
                return null;
            }

            if (spriteCache.TryGetValue(assetId, out var cached))
            {
                return cached;
            }

            var asset = repository.GetAsset(assetId);
            if (asset == null || !asset.exists)
            {
                return null;
            }

            var filePath = repository.ResolveUnityAssetPath(asset.targetPath);
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }

            var bytes = File.ReadAllBytes(filePath);
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!texture.LoadImage(bytes))
            {
                Object.Destroy(texture);
                return null;
            }

            texture.name = assetId;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100f);
            sprite.name = assetId;
            spriteCache[assetId] = sprite;
            return sprite;
        }
    }
}
