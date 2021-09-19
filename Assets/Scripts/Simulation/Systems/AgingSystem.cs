using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class AgingSystem : ECSSystem
{
    public override void Update(ECSWorld world)
    {
        foreach (var (being, _) in world.Select<Being>())
        {
            being.Age += Time.deltaTime;
        }
    }
}