using System;
using UnityEngine;
using AdventuringRequired.ECS;

[Serializable]
public class Being : ECSComponent
{
    [SerializeField]
    private string name;

    [SerializeField]
    private float age;

    [SerializeField]
    private float health;

    public string Name { get => name; set => name = value; }
    public float Age { get => age; set => age = value; }
    public float Health { get => health; set => health = value; }
}