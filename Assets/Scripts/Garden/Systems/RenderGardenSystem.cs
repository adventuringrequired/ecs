using System;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class RenderGarden : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            var index = 0;

            foreach (var (_, plant, position, spriteRender, gameObjectReference) in world.Select<Plant, Position, SpriteRender, GameObjectReference>())
            {
                Debug.Log($"RenderGarden: entity {index}");
                index++;

                var plantObject = plant.PlantObject;

                var percentDone = Mathf.Clamp01(plant.GrowTime / plantObject.TotalTimeToGrow);

                gameObjectReference.gameObject.transform.position = position.position;
                var size = plantObject.FinalSize * percentDone;
                gameObjectReference.gameObject.transform.localScale = new Vector3(size, size, 1f);

                spriteRender.Color = Color.Lerp(plantObject.StartColor, plantObject.FinalColor, percentDone);

            }
        }
    }

}
