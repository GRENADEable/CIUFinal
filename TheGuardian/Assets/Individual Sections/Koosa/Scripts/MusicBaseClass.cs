using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class MusicBaseClass
{
    public AudioClip songClip;

    [Range(0, 1)]
    public float Songvolume;
    [Range(0, 1)]
    public float Songpitch;

    public string songClipName;

    public AudioSource audioSource;

    public bool loop;
    public bool playOnAwake;
}
