using System.Collections.Generic;
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
        List<ECSEntity> entities = world.Select<Renderable, Being>();

        foreach (var entity in entities)
        {
            var renderable = entity.GetComponent<Renderable>();
            var being = entity.GetComponent<Being>();

            renderable.Position = Vector2.Lerp(renderable.Position, renderable.Position + UnityEngine.Random.insideUnitCircle, Time.deltaTime * 25 * (1 - (being.Age / deathAge)));
        }
    }
}
