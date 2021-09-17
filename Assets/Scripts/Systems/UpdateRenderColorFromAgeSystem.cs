using System;
using System.Collections.Generic;
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
        List<ECSEntity> entities = world.Select<Being, Renderable>();

        foreach (var entity in entities)
        {
            var being = entity.GetComponent<Being>();
            var renderable = entity.GetComponent<Renderable>();

            renderable.Color = Color.Lerp(Color.green, Color.red, being.Age / deathAge);
        }
    }
}