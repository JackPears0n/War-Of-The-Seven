using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(-80f, 20f)]
    public float musicVolume;

    [Range(-80f, 20f)]
    public float sFXVolume;

    //[Range(1f, 1000f)]
    //public float pitch;

    [HideInInspector]
    public AudioSource source;

}
