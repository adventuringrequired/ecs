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
    [Range(1, 10000)]
    private int population;

    [SerializeField]
    private Sprite sprite;

    private System.Random rnd = new System.Random();

    void Awake()
    {
        world.AddSystem(new AgingSystem());
        world.AddSystem(new DeathSystem());
        world.AddSystem(new UpdateRenderColorFromAgeSystem());
        world.AddSystem(new RenderSystem(sprite));


    }

    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            double range = (double)float.MaxValue - (double)float.MinValue;
            double sample = rnd.NextDouble();
            double scaled = (sample * range) + float.MinValue;
            float ageRate = (float)scaled;

            world.AddEntity(
                new Being { Name = $"Being {i}", Age = rnd.Next(0, 95), AgeRate = ageRate },
                new Renderable { Position = UnityEngine.Random.insideUnitCircle * 40f }
            );
        }
    }

    static float NextFloat(System.Random random)
    {
        double mantissa = (random.NextDouble() * 2.0) - 1.0;
        // choose -149 instead of -126 to also generate subnormal floats (*)
        double exponent = Math.Pow(2.0, random.Next(-126, 128));
        return (float)(mantissa * exponent);
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

        [SerializeField]
        private float ageRate = 1f;

        public string Name { get => name; set => name = value; }
        public float Age { get => age; set => age = value; }
        public float Health { get => health; set => health = value; }
        public float AgeRate { get => ageRate; set => ageRate = value; }
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

                if (being.Age >= 100f)
                {
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
