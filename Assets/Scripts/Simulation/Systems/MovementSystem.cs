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
        foreach (var (being, renderable, _) in world.Select<Being, Renderable>())
        {
            renderable.Position = Vector2.Lerp(
                renderable.Position,
                renderable.Position + UnityEngine.Random.insideUnitCircle,
                Time.deltaTime * 25 * (1 - (being.Age / deathAge))
            );
        }
    }
}
