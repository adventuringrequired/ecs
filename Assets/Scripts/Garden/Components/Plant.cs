using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class Plant : ECSComponent
    {
        private PlantObject plantObject;
        private float growTime = 0f;

        public float GrowTime { get => growTime; set => growTime = value; }
        public PlantObject PlantObject { get => plantObject; set => plantObject = value; }
    }
}
