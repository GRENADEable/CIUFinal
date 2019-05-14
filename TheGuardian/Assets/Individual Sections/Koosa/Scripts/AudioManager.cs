using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    // public AudioSource backGroundMusicSource;
    // public AudioSource effectSource;
    public MusicBaseClass[] sounds;
    // public MusicBaseClass[] effectClips;

    void Awake()
    {
        foreach (MusicBaseClass s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        // foreach (MusicBaseClass m in effectClips)
        // {
        //     m.source.clip = m.clip;
        //     m.source.volume = m.volume;
        //     m.source.pitch = m.pitch;
        //     m.source.loop = m.loop;
        // }

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void Play(string name)
    {
        MusicBaseClass s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null)
            Debug.LogWarning("Sound: " + name + " not found");

        s.source.Play();
    }

    public void Stop(string name)
    {
        MusicBaseClass s = Array.Find(sounds, sound => sound.clipName == name);

        if (s == null)
            Debug.LogWarning("Sound: " + name + " not found");

        s.source.Stop();
    }


    // public void PlaySingleMusicClip()
    // {
    //     backGroundMusicSource.Play();
    // }

    // public void StopSingleMusicClip()
    // {
    //     backGroundMusicSource.Stop();
    // }

    // public void PlaySingleMusicClipPoint(float startTime, float endTime)
    // {
    //     backGroundMusicSource.PlayScheduled(startTime);
    //     backGroundMusicSource.SetScheduledEndTime(AudioSettings.dspTime + (endTime));
    // }

    // public void PlaySingleEffect()
    // {
    //     effectSource.Play();
    // }

    // public void StopSingleEffect()
    // {
    //     effectSource.Stop();
    // }

    // public void PlaySingleEffectPoint(float startTime, float endTime)
    // {
    //     effectSource.PlayScheduled(startTime);
    //     effectSource.SetScheduledEndTime(AudioSettings.dspTime + (endTime));
    // }

    // public void MusicShuffle()
    // {
    //     int randomIndex = Random.Range(0, backGroundMusicClips.Length);
    //     for (int i = 0; i < backGroundMusicClips.Length; i++)
    //     {
    //         AudioClip tempClip = backGroundMusicClips[i].songClip;
    //         backGroundMusicClips[i] = backGroundMusicClips[randomIndex];
    //         backGroundMusicClips[randomIndex].songClip = tempClip;
    //     }
    //     backGroundMusicSource.clip = backGroundMusicClips[randomIndex].songClip;
    //     backGroundMusicSource.Play();
    // }

    // public void PlaySFX(AudioSource sourcePlay, int index, float startTime, float endTime, AudioClip[] audioClips)// use this for music 
    // {
    //     sourcePlay.clip = audioClips[index];
    //     sourcePlay.SetScheduledStartTime(startTime);
    //     sourcePlay.SetScheduledEndTime(endTime);
    //     sourcePlay.Play();
    // }

    // public void Playeffect(int index)// use this for effects
    // {
    //     effectSource.PlayOneShot(effectClips[index].songClip);
    // }
}
