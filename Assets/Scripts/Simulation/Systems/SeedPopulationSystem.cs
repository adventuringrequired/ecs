using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

public class SeedPopulationSystem : ECSSystem
{
    private System.Random rnd = new System.Random();

    private int population;
    private float deathAge;
    private float radius;

    public SeedPopulationSystem(int population, float radius, float deathAge)
    {
        this.population = population;
        this.radius = radius;
        this.deathAge = deathAge;
    }

    public override void Start(ECSWorld world)
    {
        for (int i = 0; i < population; i++)
        {
            world.AddEntity(
                new Being { Name = $"Being {i}", Age = rnd.Next(0, Mathf.RoundToInt(deathAge)) },
                new Renderable { Position = UnityEngine.Random.insideUnitCircle * radius }
            );
        }
    }
}
