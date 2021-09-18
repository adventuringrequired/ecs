using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class AllocateGameObjects : ECSSystem
    {
        private Dictionary<ECSEntity, GameObject> cache = new Dictionary<ECSEntity, GameObject>();

        public override void Update(ECSWorld world)
        {
            world.Select<GameObjectReference>().ForEach(match =>
            {
                var (entity, components) = match;

                if (cache.ContainsKey(entity)) return;

                GameObject gameObject = new GameObject();
                cache.Add(entity, gameObject);

                var gameObjectReference = components.Item1;
                gameObjectReference.gameObject = gameObject;

                if (gameObjectReference.name != null)
                {
                    gameObject.name = gameObjectReference.name;
                }
            });
        }
    }

}

