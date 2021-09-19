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
            var (_, being) = match;

            being.Age += Time.deltaTime;
        });
    }
}