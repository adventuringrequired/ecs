using System;
using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class RenderGarden : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            world.Select<Plant, Position, SpriteRender, GameObjectReference>().ForEach(match =>
            {
                var (_, (plant, position, spriteRender, gameObjectReference)) = match;

                var plantObject = plant.PlantObject;

                var percentDone = Mathf.Clamp01(plant.GrowTime / plantObject.TotalTimeToGrow);

                var gameObject = gameObjectReference.gameObject;
                gameObject.transform.position = position.position;
                var size = plantObject.FinalSize * percentDone;
                gameObject.transform.localScale = new Vector3(size, size, 1f);

                spriteRender.Color = Color.Lerp(plantObject.StartColor, plantObject.FinalColor, percentDone);
            });
        }
    }

}
