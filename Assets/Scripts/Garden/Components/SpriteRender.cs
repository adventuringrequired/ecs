using UnityEngine;
using AdventuringRequired.ECS;

namespace Simulations.Garden
{
    public class SpriteRender : ECSComponent
    {
        private Sprite sprite;
        private SpriteRenderer spriteRenderer;
        private Color color;

        public Sprite Sprite { get => sprite; set => sprite = value; }
        public Color Color { get => color; set => color = value; }
        public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    }

}
