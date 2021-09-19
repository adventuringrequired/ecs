using System;
using AdventuringRequired.ECS;

[Serializable]
public class DeathSystem : ECSSystem
{
    private float deathAge;

    public DeathSystem(float deathAge)
    {
        this.deathAge = deathAge;
    }

    public override void Update(ECSWorld world)
    {
        foreach (var (being, entity) in world.Select<Being>())
        {
            if (being.Age >= deathAge)
            {
                world.RemoveEntity(entity);
            }
        }
    }
}