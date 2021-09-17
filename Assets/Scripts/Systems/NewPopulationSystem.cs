﻿using System;
using System.Collections;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class NewPopulationSystem : ECSSystem
{
    private ECSWorld world;

    public override void FixedUpdate(ECSWorld world)
    {
    }

    public override void Start(ECSWorld world)
    {
        this.world = world;
        this.world.StartCoroutine(SpawnNewPopulation());
    }

    public override void Update(ECSWorld world)
    {

    }

    IEnumerator SpawnNewPopulation()
    {
        world.AddEntity(
            new Being { Name = $"Being", Age = 0 },
            new Renderable { Position = UnityEngine.Random.insideUnitCircle }
        );

        yield return new WaitForSeconds(1f);
    }
}