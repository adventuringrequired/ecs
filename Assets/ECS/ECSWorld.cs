using System;
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

        public IEnumerable<Tuple<ECSEntity, T>> Select<T>()
            where T : ECSComponent
        {
            return entities
                .Where(e => e.HasComponent<T>())
                .Select(e => Tuple.Create(e, e.GetComponent<T>()));
        }

        public IEnumerable<Tuple<ECSEntity, T, T2>> Select<T, T2>()
            where T : ECSComponent
            where T2 : ECSComponent
        {
            return entities
                .Where(e => e.HasComponent<T>() && e.HasComponent<T2>())
                .Select(e => Tuple.Create(e, e.GetComponent<T>(), e.GetComponent<T2>()));
        }

        public IEnumerable<Tuple<ECSEntity, T, T2, T3>> Select<T, T2, T3>()
            where T : ECSComponent
            where T2 : ECSComponent
            where T3 : ECSComponent
        {
            return entities
                .Where(e => e.HasComponent<T>() && e.HasComponent<T2>() && e.HasComponent<T3>())
                .Select(e => Tuple.Create(e, e.GetComponent<T>(), e.GetComponent<T2>(), e.GetComponent<T3>()));
        }

        public IEnumerable<Tuple<ECSEntity, T, T2, T3, T4>> Select<T, T2, T3, T4>()
            where T : ECSComponent
            where T2 : ECSComponent
            where T3 : ECSComponent
            where T4 : ECSComponent
        {
            return entities
                .Where(e => e.HasComponent<T>() && e.HasComponent<T2>() && e.HasComponent<T3>() && e.HasComponent<T4>())
                .Select(e => Tuple.Create(e, e.GetComponent<T>(), e.GetComponent<T2>(), e.GetComponent<T3>(), e.GetComponent<T4>()));
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

