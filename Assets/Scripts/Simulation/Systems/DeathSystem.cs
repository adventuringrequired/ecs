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
        var matches = world.Select<Being, Renderable>();

        foreach (var match in matches)
        {
            var entity = match.Item1;
            var being = match.Item2.Item1;

            if (being.Age >= deathAge)
            {
                world.RemoveEntity(entity);
            }
        }
    }
}