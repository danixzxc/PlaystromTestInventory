using System;
using UnityEngine;

[Serializable]
public struct SoundEntry
{
    public SoundID Id;
    public AudioClip Clip;
    [Range(0.8f, 1.2f)]
    public float MinPitch;
    [Range(0.8f, 1.2f)]
    public float MaxPitch;
    [Range(0f, 1f)]
    public float Volume;
}