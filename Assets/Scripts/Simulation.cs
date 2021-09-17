using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

public class Simulation : MonoBehaviour
{
    [SerializeField]
    private ECSWorld world;

    [SerializeField]
    [Range(1, 25000)]
    private int population = 2500;

    [SerializeField]
    private float deathAge = 60f;

    [SerializeField]
    [Range(1f, 50f)]
    private float startingRadius = 10f;

    [SerializeField]
    private Sprite sprite;

    private System.Random rnd = new System.Random();

    void Awake()
    {
        world.AddSystem(new AgingSystem());
        world.AddSystem(new DeathSystem(deathAge));
        world.AddSystem(new UpdateRenderColorFromAgeSystem(deathAge));
        world.AddSystem(new RenderSystem(sprite));
        world.AddSystem(new MovementSystem());
    }

    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            world.AddEntity(
                new Being { Name = $"Being {i}", Age = rnd.Next(0, Mathf.RoundToInt(deathAge)) },
                new Renderable { Position = UnityEngine.Random.insideUnitCircle * startingRadius }
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
        private float deathAge;

        public DeathSystem(float deathAge)
        {
            this.deathAge = deathAge;
        }

        public override void Start(ECSWorld world) { }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being, Renderable>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();

                if (being.Age >= deathAge)
                {
                    world.RemoveEntity(entity);
                }
            }
        }
    }

    [Serializable]
    private class UpdateRenderColorFromAgeSystem : ECSSystem
    {
        private float deathAge;

        public UpdateRenderColorFromAgeSystem(float deathAge)
        {
            this.deathAge = deathAge;
        }

        public override void Start(ECSWorld world) { }

        public override void FixedUpdate(ECSWorld world) { }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Being, Renderable>();

            foreach (var entity in entities)
            {
                var being = entity.GetComponent<Being>();
                var renderable = entity.GetComponent<Renderable>();

                renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / deathAge);
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

    [SerializeField]
    private class MovementSystem : ECSSystem
    {
        public override void FixedUpdate(ECSWorld world)
        {
        }

        public override void Start(ECSWorld world)
        {
        }

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<Renderable>();

            foreach (var entity in entities)
            {
                var renderable = entity.GetComponent<Renderable>();



                renderable.Position = Vector2.Lerp(renderable.Position, renderable.Position + UnityEngine.Random.insideUnitCircle, Time.deltaTime * 25);
            }
        }
    }
}
