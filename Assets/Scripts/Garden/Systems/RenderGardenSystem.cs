using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class RenderGarden : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            var matches = world.Select<Plant, Position, SpriteRender, GameObjectReference>();

            foreach (var match in matches)
            {
                var plant = match.Item2.Item1;
                var position = match.Item2.Item2;
                var spriteRender = match.Item2.Item3;
                var gameObjectReference = match.Item2.Item4;

                var gameObject = gameObjectReference.gameObject;
                var plantObject = plant.PlantObject;

                var percentDone = Mathf.Clamp01(plant.GrowTime / plant.PlantObject.TotalTimeToGrow);

                gameObject.transform.position = position.position;

                var size = plantObject.FinalSize * percentDone;
                gameObject.transform.localScale = new Vector3(size, size, 1f);

                spriteRender.Color = Color.Lerp(plantObject.StartColor, plantObject.FinalColor, percentDone);
            }
        }
    }

}
