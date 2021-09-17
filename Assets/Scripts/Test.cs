using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

public class Test : MonoBehaviour
{
    [SerializeField]
    private ECSWorld world;

    [SerializeField]
    private Sprite sprite;

    void Awake()
    {
        world.AddSystem(new GreetingSystem());
        world.AddSystem(new AgingSystem());
        world.AddSystem(new DeathSystem());
        world.AddSystem(new UpdateRenderColorFromAgeSystem());
        world.AddSystem(new RenderSystem(sprite));

        world.AddEntity(
            new Being { Name = "Test Person", Age = 3 },
            new Renderable { Color = Color.cyan }
        );

        world.AddEntity(
            new Being { Name = "Test Person 2", Age = 1 },
            new Renderable { Color = Color.green, Position = new Vector2(5f, 5f) }
        );
    }

    [Serializable]
    private class Being : ECSComponent
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private float age;

        [SerializeField]
        private float health;

        public string Name { get => name; set => name = value; }
        public float Age { get => age; set => age = value; }
        public float Health { get => health; set => health = value; }
    }

    [Serializable]
    private class Renderable : ECSComponent
    {
        [SerializeField]
        private Vector2 position = Vector2.zero;

        [SerializeField]
        private Vector2 size = Vector2.one;

        [SerializeField]
        private Color color;

        public Vector2 Position { get => position; set => position = value; }
        public Color Color { get => color; set => color = value; }
        public Vector2 Size { get => size; set => size = value; }
    }

    [Serializable]
    private class GreetingSystem : ECSSystem
    {
        public override void Start(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();
                Debug.Log($"Hello, {being.Name}");
            }
        }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world) { }
    }

    [Serializable]
    private class AgingSystem : ECSSystem
    {
        public override void Start(ECSWorld world) { }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();
                being.Age += Time.deltaTime;
            }
        }
    }

    [Serializable]
    private class DeathSystem : ECSSystem
    {
        public override void Start(ECSWorld world) { }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being, Renderable>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();

                if (being.Age > 100f)
                {
                    Debug.Log($"Being {being.Name} has passed");
                    world.RemoveEntity(entity);
                }
            }
        }
    }

    [Serializable]
    private class UpdateRenderColorFromAgeSystem : ECSSystem
    {
        public override void Start(ECSWorld world) { }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being, Renderable>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();
                var renderable = entity.GetComponent<Renderable>();

                renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / 100);
            }
        }
    }

    [Serializable]
    private class RenderSystem : ECSSystem
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
}
