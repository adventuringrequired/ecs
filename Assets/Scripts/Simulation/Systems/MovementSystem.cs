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
        var matches = world.Select<Renderable, Being>();

        foreach (var match in matches)
        {
            var renderable = match.Item2.Item1;
            var being = match.Item2.Item2;

            renderable.Position = Vector2.Lerp(renderable.Position, renderable.Position + UnityEngine.Random.insideUnitCircle, Time.deltaTime * 25 * (1 - (being.Age / deathAge)));
        }
    }
}
