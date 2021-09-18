using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class UpdateRenderColorFromAgeSystem : ECSSystem
{
    private float deathAge;

    public UpdateRenderColorFromAgeSystem(float deathAge)
    {
        this.deathAge = deathAge;
    }

    public override void Update(ECSWorld world)
    {
        world.Select<Being, Renderable>().ForEach(match =>
        {
            var (_, (being, renderable)) = match;

            renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / deathAge);
        });
    }
}