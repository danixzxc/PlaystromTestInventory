
using Data.ScriptableObjects;
using System;
using UnityEngine;

[Serializable]
public struct DropChance
{
    public DropItemConfig ItemConfig;
    [Range(0f, 1f)]
    public float Weight;
}