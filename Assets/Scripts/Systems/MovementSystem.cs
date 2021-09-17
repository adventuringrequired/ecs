using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

[SerializeField]
public class MovementSystem : ECSSystem
{
    public override void FixedUpdate(ECSWorld world)
    {
    }

    public override void Start(ECSWorld world)
    {
    }

    public override void Update(ECSWorld world)
    {
        List<ECSEntity> entities = world.Select<Renderable>();

        foreach (var entity in entities)
        {
            var renderable = entity.GetComponent<Renderable>();

            renderable.Position = Vector2.Lerp(renderable.Position, renderable.Position + UnityEngine.Random.insideUnitCircle, Time.deltaTime * 25);
        }
    }
}
