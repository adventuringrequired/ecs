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
        world.Select<Being>().ForEach(match =>
        {
            var (entity, components) = match;
            var being = components.Item1;

            if (being.Age >= deathAge)
            {
                world.RemoveEntity(entity);
            }
        });
    }
}