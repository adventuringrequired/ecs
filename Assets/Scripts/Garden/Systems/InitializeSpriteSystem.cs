using System;
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
            world.Select<SpriteRender, GameObjectReference>().ForEach(match =>
            {
                var (_, (spriteRender, gameObjectReference)) = match;

                var gameObject = gameObjectReference.gameObject;

                if (!spriteCache.ContainsKey(gameObject))
                {
                    var renderer = gameObject.AddComponent<SpriteRenderer>();
                    spriteCache.Add(gameObject, renderer);
                    renderer.sprite = sprite;
                    renderer.color = spriteRender.Color;
                }

                if (spriteCache.TryGetValue(gameObject, out var spriteRenderer))
                {
                    spriteRenderer.color = spriteRender.Color;
                }
            });
        }
    }

}
