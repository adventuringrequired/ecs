using System;
using System.Collections.Generic;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class AgingSystem : ECSSystem
{
    public override void Start(ECSWorld world) { }

    public override void FixedUpdate(ECSWorld world) { }

    public override void Update(ECSWorld world)
    {
        List<ECSEntity> entities = world.Select<Being>();

        foreach (var entity in entities)
        {
            var being = entity.GetComponent<Being>();
            being.Age += Time.deltaTime;
        }
    }
}