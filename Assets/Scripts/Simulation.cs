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
        world.AddSystem(new NewPopulationSystem());
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
}
