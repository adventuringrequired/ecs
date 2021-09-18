using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventuringRequired.ECS
{
    [System.Serializable]
    public class ECSWorld : MonoBehaviour
    {
        [SerializeField]
        private Dictionary<ECSEntity, GameObject> gameObjectCache = new Dictionary<ECSEntity, GameObject>();

        [SerializeField]
        private List<ECSEntity> entities = new List<ECSEntity>();

        [SerializeField]
        private List<ECSSystem> systems = new List<ECSSystem>();

        public List<ECSEntity> Entities { get => entities; set => entities = value; }

        public void AddEntity(params ECSComponent[] components)
        {
            var entity = new ECSEntity(components);
            entities.Add(entity);
        }

        public void CacheGameObject(ECSEntity entity, GameObject gameObject)
        {
            gameObjectCache.Add(entity, gameObject);
        }

        public bool TryGetCacheGameObject(ECSEntity entity, out GameObject gameObject)
        {
            return gameObjectCache.TryGetValue(entity, out gameObject);
        }

        public void RemoveEntity(ECSEntity entity)
        {
            if (gameObjectCache.TryGetValue(entity, out var gameObject))
            {
                GameObject.Destroy(gameObject);
            }

            entities.Remove(entity);
        }

        public void AddSystem(ECSSystem system)
        {
            systems.Add(system);
        }

        public void AddSystems(params ECSSystem[] systemsToAdd)
        {
            systems.AddRange(systemsToAdd);
        }

        public List<ECSEntity> Select<T>()
            where T : ECSComponent
        {
            return entities.Select(e =>
            {
                if (e.HasComponent<T>())
                {
                    return e;
                }

                return null;
            }).ToList();
        }

        public List<ECSEntity> Select<T, T2>()
            where T : ECSComponent
            where T2 : ECSComponent
        {
            return entities.Select(e =>
            {
                if (e.HasComponent<T>() && e.HasComponent<T2>())
                {
                    return e;
                }

                return null;
            }).ToList();
        }

        public List<ECSEntity> Select<T, T2, T3>()
            where T : ECSComponent
            where T2 : ECSComponent
            where T3 : ECSComponent
        {
            return entities.Select(e =>
            {
                if (e.HasComponent<T>() && e.HasComponent<T2>() && e.HasComponent<T3>())
                {
                    return e;
                }

                return null;
            }).ToList();
        }

        public List<ECSEntity> Select<T, T2, T3, T4>()
            where T : ECSComponent
            where T2 : ECSComponent
            where T3 : ECSComponent
            where T4 : ECSComponent
        {
            return entities.Select(e =>
            {
                if (e.HasComponent<T>() && e.HasComponent<T2>() && e.HasComponent<T3>() && e.HasComponent<T4>())
                {
                    return e;
                }

                return null;
            }).ToList();
        }

        void Start()
        {
            foreach (var system in systems)
            {
                system.Start(this);
            }
        }

        void Update()
        {
            foreach (var system in systems)
            {
                system.Update(this);
            }
        }

        void FixedUpdate()
        {
            foreach (var system in systems)
            {
                system.FixedUpdate(this);
            }
        }
    }
}

