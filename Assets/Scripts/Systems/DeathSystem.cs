using System;
using System.Collections.Generic;
using AdventuringRequired.ECS;

[Serializable]
public class DeathSystem : ECSSystem
{
    private float deathAge;

    public DeathSystem(float deathAge)
    {
        this.deathAge = deathAge;
    }

    public override void Start(ECSWorld world) { }

    public override void FixedUpdate(ECSWorld world) { }

    public override void Update(ECSWorld world)
    {
        List<ECSEntity> entities = world.Select<Being, Renderable>();

        foreach (var entity in entities)
        {
            var being = entity.GetComponent<Being>();

            if (being.Age >= deathAge)
            {
                world.RemoveEntity(entity);
            }
        }
    }
}