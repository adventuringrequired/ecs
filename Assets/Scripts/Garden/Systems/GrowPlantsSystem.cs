using System;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class GrowPlants : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            foreach (var (plant, _) in world.Select<Plant>())
            {
                plant.GrowTime += Time.deltaTime;
            }
        }
    }
}


