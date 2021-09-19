using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;
using System.Linq;

namespace Simulations.Garden
{
    public class AllocateGameObjects : ECSSystem
    {
        private Dictionary<ECSEntity, GameObject> cache = new Dictionary<ECSEntity, GameObject>();

        public override void Update(ECSWorld world)
        {
            var matches = world.Select<GameObjectReference>();

            Debug.Log($"AllocateGameObjects: entity matches {matches.Count()}");

            var index = 0;

            foreach (var (gameObjectReference, entity) in matches)
            {
                Debug.Log($"AllocateGameObjects: entity {index}: Checking...");
                index++;

                if (gameObjectReference.gameObject != null)
                {
                    continue;
                };

                Debug.Log($"AllocateGameObjects: entity {index}: Allocating game object");

                gameObjectReference.gameObject = new GameObject();

                if (gameObjectReference.name != null)
                {
                    gameObjectReference.gameObject.name = gameObjectReference.name;
                }

            }
        }
    }

}

