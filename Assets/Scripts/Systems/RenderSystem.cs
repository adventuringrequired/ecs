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

    public override void Start(ECSWorld world) { }

    public override void FixedUpdate(ECSWorld world) { }

    public override void Update(ECSWorld world)
    {
        List<ECSEntity> entities = world.Select<Renderable>();

        foreach (var entity in entities)
        {
            GameObject gameObject;

            if (!world.TryGetCacheGameObject(entity, out gameObject))
            {
                gameObject = new GameObject();
                world.CacheGameObject(entity, gameObject);

                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
                spriteCache.Add(gameObject, spriteRenderer);

                spriteRenderer.sprite = sprite;
            }

            var renderable = entity.GetComponent<Renderable>();

            gameObject.transform.position = renderable.Position;
            gameObject.transform.localScale = renderable.Size;

            if (spriteCache.TryGetValue(gameObject, out var spriteRender))
            {
                spriteRender.color = renderable.Color;
            }
        }
    }
}