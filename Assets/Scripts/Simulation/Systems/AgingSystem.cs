using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class AgingSystem : ECSSystem
{
    public override void Update(ECSWorld world)
    {
        var matches = world.Select<Being>();

        foreach (var match in matches)
        {
            var being = match.Item2.Item1;
            being.Age += Time.deltaTime;
        }
    }
}