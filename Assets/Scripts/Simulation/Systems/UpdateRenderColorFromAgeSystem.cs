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
        var matches = world.Select<Being, Renderable>();

        foreach (var match in matches)
        {
            var being = match.Item2.Item1;
            var renderable = match.Item2.Item2;

            renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / deathAge);
        }
    }
}