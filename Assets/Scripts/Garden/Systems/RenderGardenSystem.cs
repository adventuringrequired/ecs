using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class RenderGarden : ECSSystem
    {
        public override void Update(ECSWorld world)
        {
            var entities = world.Select<Plant, Position, SpriteRender, GameObjectReference>();

            foreach (var entity in entities)
            {
                var plant = entity.GetComponent<Plant>();
                var position = entity.GetComponent<Position>();
                var spriteRender = entity.GetComponent<SpriteRender>();
                var gameObjectReference = entity.GetComponent<GameObjectReference>();

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
