using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuringRequired.ECS
{
    [Serializable]
    public class ECSEntity
    {
        [SerializeField]
        private System.Guid id;

        [SerializeField]
        private List<ECSComponent> components;

        public Guid Id { get => id; }

        public ECSEntity(Guid id, params ECSComponent[] components)
        {
            this.id = id;
            this.components = new List<ECSComponent>(components);
        }

        public ECSEntity(params ECSComponent[] components)
        {
            this.id = System.Guid.NewGuid();
            this.components = new List<ECSComponent>(components);
        }

        public ECSEntity()
        {
            this.id = System.Guid.NewGuid();
            this.components = new List<ECSComponent>();
        }

        public T GetComponent<T>() where T : ECSComponent
        {
            foreach (ECSComponent component in components)
            {
                if (component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }

            throw new ArgumentOutOfRangeException("T", $"Unable to get component of type {typeof(T)}");
        }

        public bool TryGetComponent<T>(out ECSComponent component) where T : ECSComponent
        {
            try
            {
                component = GetComponent<T>();
                return true;
            }
            catch (Exception)
            {
                component = null;
                return false;
            }
        }

        public bool HasComponent<T>() where T : ECSComponent
        {
            try
            {
                GetComponent<T>();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
