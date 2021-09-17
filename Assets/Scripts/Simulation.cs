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

    void Awake()
    {
        world.AddSystems(
            new SeedPopulationSystem(population, startingRadius, deathAge),
            new AgingSystem(),
            new DeathSystem(deathAge),
            new UpdateRenderColorFromAgeSystem(deathAge),
            new RenderSystem(sprite),
            new MovementSystem(deathAge),
            new NewPopulationSystem()
        );
    }
}
