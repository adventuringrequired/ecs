using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class GrowPlants : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            var entities = world.Select<Plant>();

            foreach (var entity in entities)
            {
                var plant = entity.GetComponent<Plant>();

                plant.GrowTime += Time.deltaTime;
            }
        }
    }
}


