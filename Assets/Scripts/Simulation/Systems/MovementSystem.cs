using System;
using UnityEngine;
using AdventuringRequired.ECS;

[SerializeField]
public class MovementSystem : ECSSystem
{
    private float deathAge;

    public MovementSystem(float deathAge)
    {
        this.deathAge = deathAge;
    }

    public override void Update(ECSWorld world)
    {
        world.Select<Being, Renderable>().ForEach(match =>
        {
            var (_, being, renderable) = match;

            renderable.Position = Vector2.Lerp(
                renderable.Position,
                renderable.Position + UnityEngine.Random.insideUnitCircle,
                Time.deltaTime * 25 * (1 - (being.Age / deathAge))
            );
        });
    }
}
