using System.Collections;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class RandomPlantingSystem : ECSSystem
    {
        private ECSWorld world;
        private PlantObject plantObject;

        private float plantingRadius = 1f;

        private float timeBetweenPlantings;

        public RandomPlantingSystem(PlantObject plantObject, float plantingRadius, float timeBetweenPlantings)
        {
            this.plantObject = plantObject;
            this.plantingRadius = plantingRadius;
            this.timeBetweenPlantings = timeBetweenPlantings;
        }

        public override void Start(ECSWorld world)
        {
            this.world = world;
            this.world.StartCoroutine(RandomlyPlantPlants());
        }

        IEnumerator RandomlyPlantPlants()
        {
            while (true)
            {
                world.AddEntity(
                    new Plant { PlantObject = plantObject },
                    new Position { position = Random.insideUnitCircle * plantingRadius },
                    new GameObjectReference { name = plantObject.PlantName },
                    new SpriteRender()
                );

                yield return new WaitForSeconds(timeBetweenPlantings);
            }
        }
    }
}

