using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerKhatim : MonoBehaviour
{
    // public static AudioManager instance = null;
    public AudioSource fxSource;
    public AudioSource mainMenuOST;
    public AudioSource atticSceneOST;
    public AudioSource hallwaySceneOST;
    public AudioSource nurserySceneOST;
    public AudioClip[] soundClips;

    void OnEnable()
    {
        // SceneManage.onStopAudioForMainMenu += OnStopAudioForMainMenu;
        // SceneManage.onAtticSceneMusic += OnAtticSceneMusicReceived;
        // SceneManage.onHallwaySceneMusic += OnHallwaySceneMusicReceived;
        // SceneManage.onNurserySceneMusic += OnNurserySceneMusicReceived;

        // PlayerControls.onPlayHallwayOST += OnHallwaySceneMusicReceived;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnDisable()
    {
        // SceneManage.onStopAudioForMainMenu -= OnStopAudioForMainMenu;
        // SceneManage.onAtticSceneMusic -= OnAtticSceneMusicReceived;
        // SceneManage.onHallwaySceneMusic -= OnHallwaySceneMusicReceived;
        // SceneManage.onNurserySceneMusic -= OnNurserySceneMusicReceived;

        // PlayerControls.onPlayHallwayOST -= OnHallwaySceneMusicReceived;
    }

    void OnDestroy()
    {
        // SceneManage.onStopAudioForMainMenu -= OnStopAudioForMainMenu;
        // SceneManage.onAtticSceneMusic -= OnAtticSceneMusicReceived;
        // SceneManage.onHallwaySceneMusic -= OnHallwaySceneMusicReceived;
        // SceneManage.onNurserySceneMusic -= OnNurserySceneMusicReceived;

        // PlayerControls.onPlayHallwayOST -= OnHallwaySceneMusicReceived;
    }

    void StopAudio()
    {
        fxSource.Stop();
        mainMenuOST.Stop();
        atticSceneOST.Stop();
        hallwaySceneOST.Stop();
        nurserySceneOST.Stop();
    }

    void AudioAccess(int index)
    {
        fxSource.PlayOneShot(soundClips[index]);
    }

    // void OnAtticSceneMusicReceived()
    // {
    //     StopAudio();
    //     atticSceneOST.Play();
    //     atticSceneOST.loop = true;
    // }

    // void OnHallwaySceneMusicReceived()
    // {
    //     StopAudio();
    //     hallwaySceneOST.Play();
    //     hallwaySceneOST.loop = true;
    // }

    // void OnNurserySceneMusicReceived()
    // {
    //     StopAudio();
    //     nurserySceneOST.Play();
    //     nurserySceneOST.loop = true;
    // }

    // void OnStopAudioForMainMenu()
    // {
    //     StopAudio();
    // }
}
