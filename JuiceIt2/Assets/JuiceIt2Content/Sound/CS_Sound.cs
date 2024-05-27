using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound",menuName = "Scriptables/Sound")]

public class CS_Sound : ScriptableObject
{
    [Header("Parameters")]
    public AudioClip audioClip;
    [Range(0f, 1f)] public float minVolume;
    [Range(0f, 1f)] public float maxVolume;
    [Range(-3f, 3f)] public float minPitch;
    [Range(-3f, 3f)] public float maxPitch;
    [Range(-1f, 1f)] public float panStereo;
}
