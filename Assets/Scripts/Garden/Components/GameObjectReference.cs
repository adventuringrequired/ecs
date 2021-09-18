using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;


namespace Simulations.Garden
{
    public class GameObjectReference : ECSComponent
    {
        public GameObject gameObject;
        public string name;
    }

}
