using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class AgingSystem : ECSSystem
{
    public override void Update(ECSWorld world)
    {
        foreach (var (_, being) in world.Select<Being>())
        {
            being.Age += Time.deltaTime;
        }
    }
}