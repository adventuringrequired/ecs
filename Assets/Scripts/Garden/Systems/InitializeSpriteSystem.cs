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
            foreach (var (spriteRender, gameObjectReference, _) in world.Select<SpriteRender, GameObjectReference>())
            {
                if (spriteRender.SpriteRenderer == null)
                {
                    spriteRender.SpriteRenderer = gameObjectReference.gameObject.AddComponent<SpriteRenderer>();
                    spriteRender.SpriteRenderer.sprite = sprite;
                }

                spriteRender.SpriteRenderer.color = spriteRender.Color;
            }
        }
    }

}
