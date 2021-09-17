using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class Renderable : ECSComponent
{
    [SerializeField]
    private Vector2 position = Vector2.zero;

    [SerializeField]
    private Vector2 size = Vector2.one;

    [SerializeField]
    private Color color;

    public Vector2 Position { get => position; set => position = value; }
    public Color Color { get => color; set => color = value; }
    public Vector2 Size { get => size; set => size = value; }
}