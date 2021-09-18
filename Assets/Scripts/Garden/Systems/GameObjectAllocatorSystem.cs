using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class AllocateGameObjects : ECSSystem
    {
        private Dictionary<ECSEntity, GameObject> cache = new Dictionary<ECSEntity, GameObject>();

        public override void Update(ECSWorld world)
        {
            List<ECSEntity> entities = world.Select<GameObjectReference>();

            foreach (var entity in entities)
            {
                if (cache.ContainsKey(entity)) continue;

                var gameObjectReference = entity.GetComponent<GameObjectReference>();

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

