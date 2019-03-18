using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager auidoInstance;
    public AudioSource backGroundMusicSource;
    public AudioSource effectSource;
    public MusicBaseClass[] backGroundMusicClips;
    public MusicBaseClass[] effectClips;
    
    void Awake()
    {
        foreach(MusicBaseClass m in backGroundMusicClips)
        {
            m.audioSource.gameObject.AddComponent<AudioSource>();
            m.audioSource.clip = m.songClip;
            m.audioSource.volume = m.Songvolume;
            m.audioSource.pitch = m.Songpitch;
            m.audioSource.loop = m.loop;
            m.audioSource.playOnAwake = m.playOnAwake;
        }
        foreach (MusicBaseClass m in effectClips)
        {
            m.audioSource.gameObject.AddComponent<AudioSource>();
            m.audioSource.clip = m.songClip;
            m.audioSource.volume = m.Songvolume;
            m.audioSource.pitch = m.Songpitch;
            m.audioSource.loop = m.loop;
            m.audioSource.playOnAwake = m.playOnAwake;
        }

        if (auidoInstance == null)
            auidoInstance = this;

        else if (auidoInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingleMusicClip()
    {
        backGroundMusicSource.Play();
    }
    void Update()
    {
        if (!backGroundMusicSource.isPlaying) { }
        //MusicShuffle();
    }

    public void StopSingleMusicClip()
    {
        backGroundMusicSource.Stop();
    }

    public void PlaySingleMusicClipPoint(float startTime, float endTime)
    {
        backGroundMusicSource.PlayScheduled(startTime);
        backGroundMusicSource.SetScheduledEndTime(AudioSettings.dspTime + (endTime));
    }

    public void PlaySingleEffect()
    {
        effectSource.Play();
    }

    public void StopSingleEffect()
    {
        effectSource.Stop();
    }

    public void PlaySingleEffectPoint(float startTime, float endTime)
    {
        effectSource.PlayScheduled(startTime);
        effectSource.SetScheduledEndTime(AudioSettings.dspTime + (endTime));
    }

    public void MusicShuffle()
    {
        int randomIndex = Random.Range(0, backGroundMusicClips.Length);
        for (int i = 0; i < backGroundMusicClips.Length; i++)
        {
            AudioClip tempClip = backGroundMusicClips[i].songClip;
            backGroundMusicClips[i] = backGroundMusicClips[randomIndex];
            backGroundMusicClips[randomIndex].songClip = tempClip;
        }
        backGroundMusicSource.clip = backGroundMusicClips[randomIndex].songClip;
        backGroundMusicSource.Play();
    }

    public void PlaySFX(AudioSource sourcePlay, int index, float startTime, float endTime, AudioClip[] audioClips)
    {
        sourcePlay.clip = audioClips[index];
        sourcePlay.SetScheduledStartTime(startTime);
        sourcePlay.SetScheduledEndTime(endTime);
        sourcePlay.Play();
    }

    public void Playeffect(int index)
    {
        effectSource.PlayOneShot(effectClips[index].songClip);
    }

    public void PlaySpawn(int index)
    {
        backGroundMusicSource.PlayOneShot(effectClips[index].songClip);
    }
}
