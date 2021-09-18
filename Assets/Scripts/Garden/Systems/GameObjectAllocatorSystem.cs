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
            var matches = world.Select<GameObjectReference>();

            foreach (var match in matches)
            {
                var entity = match.Item1;

                if (cache.ContainsKey(entity)) continue;

                var gameObjectReference = match.Item2.Item1;

                GameObject gameObject = new GameObject();
                cache.Add(entity, gameObject);

                gameObjectReference.gameObject = gameObject;

                if (gameObjectReference.name != null)
                {
                    gameObject.name = gameObjectReference.name;
                }
            }
        }
    }

}

