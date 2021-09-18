﻿using System;
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
        var matches = world.Select<Renderable>();

        foreach (var match in matches)
        {
            GameObject gameObject;

            var entity = match.Item1;

            if (!world.TryGetCacheGameObject(entity, out gameObject))
            {
                gameObject = new GameObject();
                world.CacheGameObject(entity, gameObject);

                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = spriteCache.Count;
                spriteCache.Add(gameObject, spriteRenderer);

                spriteRenderer.sprite = sprite;
            }

            var renderable = match.Item2.Item1;

            gameObject.transform.position = renderable.Position;
            gameObject.transform.localScale = renderable.Size;

            if (spriteCache.TryGetValue(gameObject, out var spriteRender))
            {
                spriteRender.color = renderable.Color;
            }
        }
    }
}