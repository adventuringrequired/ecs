namespace AdventuringRequired.ECS
{
    [System.Serializable]
    public abstract class ECSSystem
    {
        public abstract void Start(ECSWorld world);
        public abstract void Update(ECSWorld world);
        public abstract void FixedUpdate(ECSWorld world);
    }
}

