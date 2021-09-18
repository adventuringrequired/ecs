using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class AllocateSprites : ECSSystem
    {
        private Dictionary<GameObject, SpriteRenderer> spriteCache = new Dictionary<GameObject, SpriteRenderer>();

        private Sprite sprite;

        public AllocateSprites(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public override void Update(ECSWorld world)
        {
            var matches = world.Select<SpriteRender, GameObjectReference>();

            foreach (var match in matches)
            {
                var spriteRender = match.Item2.Item1;
                var gameObjectReference = match.Item2.Item2;

                if (!spriteCache.ContainsKey(gameObjectReference.gameObject))
                {
                    var renderer = gameObjectReference.gameObject.AddComponent<SpriteRenderer>();
                    spriteCache.Add(gameObjectReference.gameObject, renderer);
                    renderer.sprite = sprite;
                    renderer.color = spriteRender.Color;
                }

                if (spriteCache.TryGetValue(gameObjectReference.gameObject, out var spriteRenderer))
                {
                    spriteRenderer.color = spriteRender.Color;
                }
            }
        }
    }

}
