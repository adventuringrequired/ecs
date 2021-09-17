namespace AdventuringRequired.ECS
{
    [System.Serializable]
    public class ECSSystem
    {
        public virtual void Start(ECSWorld world) { }
        public virtual void Update(ECSWorld world) { }
        public virtual void FixedUpdate(ECSWorld world) { }
    }
}

