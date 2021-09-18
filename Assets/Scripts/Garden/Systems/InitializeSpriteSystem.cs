using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class InitializeSpriteSystem : ECSSystem
    {
        private Dictionary<GameObject, SpriteRenderer> spriteCache = new Dictionary<GameObject, SpriteRenderer>();

        private Sprite sprite;

        public InitializeSpriteSystem(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<SpriteRender, GameObjectReference>();

            foreach (var entity in entities)
            {
                var spriteRender = entity.GetComponent<SpriteRender>();
                var gameObjectReference = entity.GetComponent<GameObjectReference>();

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
