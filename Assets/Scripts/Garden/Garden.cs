using UnityEngine;
using System.Collections.Generic;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class Garden : MonoBehaviour
    {
        [SerializeField]
        private ECSWorld world;

        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private List<PlantObject> plantObjects;

        [SerializeField]
        private float plantingRadius;

        [SerializeField]
        private float timeBetweenPlantings = 60;

        void Awake()
        {
            world.AddSystems(
                new GameObjectAllocatorSystem(),
                new InitializeSpriteSystem(sprite),
                new GrowPlantsSystem(),
                new RenderGardenSystem(),
                new RandomPlantingSystem(plantObjects[0], plantingRadius, timeBetweenPlantings)
            );
        }
    }
}
