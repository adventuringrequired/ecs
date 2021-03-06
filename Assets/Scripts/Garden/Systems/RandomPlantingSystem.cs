using System.Collections;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class PlantRandomPlants : ECSSystem
    {
        private ECSWorld world;
        private PlantObject plantObject;

        private float plantingRadius = 1f;

        private float timeBetweenPlantings;

        public PlantRandomPlants(PlantObject plantObject, float plantingRadius, float timeBetweenPlantings)
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
            var index = 0;

            while (true)
            {
                Debug.Log($"PlantRandomPlants: RandomlyPlantPlants: entity {index}");
                index++;

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

