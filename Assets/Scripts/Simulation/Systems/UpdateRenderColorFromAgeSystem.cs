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
        foreach (var (_, being, renderable) in world.Select<Being, Renderable>())
        {
            renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / deathAge);
        }
    }
}