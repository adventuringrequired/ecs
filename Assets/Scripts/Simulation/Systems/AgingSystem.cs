using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class AgingSystem : ECSSystem
{
    public override void Update(ECSWorld world)
    {
        world.Select<Being>().ForEach(match =>
        {
            var (_, components) = match;
            var being = components.Item1;

            being.Age += Time.deltaTime;
        });
    }
}