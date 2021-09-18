using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class RenderSystem : ECSSystem
{
    private Dictionary<GameObject, SpriteRenderer> spriteCache = new Dictionary<GameObject, SpriteRenderer>();

    private Sprite sprite;

    public RenderSystem(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public override void Update(ECSWorld world)
    {
        world.Select<Renderable>().ForEach(match =>
        {
            var (entity, components) = match;
            var renderable = components.Item1;

            GameObject gameObject;

            if (!world.TryGetCacheGameObject(entity, out gameObject))
            {
                gameObject = new GameObject();
                world.CacheGameObject(entity, gameObject);

                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = spriteCache.Count;
                spriteCache.Add(gameObject, spriteRenderer);

                spriteRenderer.sprite = sprite;
            }

            gameObject.transform.position = renderable.Position;
            gameObject.transform.localScale = renderable.Size;

            if (spriteCache.TryGetValue(gameObject, out var spriteRender))
            {
                spriteRender.color = renderable.Color;
            }
        });
    }
}