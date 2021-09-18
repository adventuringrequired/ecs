using System;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class GrowPlants : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            world.Select<Plant>().ForEach(match =>
            {
                var (_, components) = match;
                var plant = components.Item1;

                plant.GrowTime += Time.deltaTime;
            });
        }
    }
}


