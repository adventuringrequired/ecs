using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class GrowPlants : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            var matches = world.Select<Plant>();

            foreach (var match in matches)
            {
                var plant = match.Item2.Item1;

                plant.GrowTime += Time.deltaTime;
            }
        }
    }
}


