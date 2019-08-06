using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public float exposureWeightAttic;
    public float exposureWeightHallway;
    public float exposureWeightNursery;
    public bool startFading;
    public bool startHallwayEndFade;
    public bool startNurseryEndFade;
    private PostProcessVolume ppVolume;

    public delegate void SendEvents();
    public static event SendEvents onUITitle;

    void OnEnable()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        EventManager.onAtticFadeOut += OnFadeOutReceived;
        PlayerControls.onHallwayFadeout += OnFadeOutReceived;
        LightEnable.onFadeOut += OnFadeOutReceived;
    }

    void OnDisable()
    {
        EventManager.onAtticFadeOut -= OnFadeOutReceived;
        PlayerControls.onHallwayFadeout -= OnFadeOutReceived;
        LightEnable.onFadeOut -= OnFadeOutReceived;
    }

    void OnDestroy()
    {
        EventManager.onAtticFadeOut -= OnFadeOutReceived;
        PlayerControls.onHallwayFadeout -= OnFadeOutReceived;
        LightEnable.onFadeOut -= OnFadeOutReceived;
    }

    void Update()
    {
        if (startFading)
        {
            exposureWeightAttic += Time.deltaTime;
            ppVolume.weight = exposureWeightAttic;

            if (exposureWeightAttic >= 1)
            {
                this.enabled = false;
            }
        }

        if (startHallwayEndFade)
        {
            exposureWeightHallway += Time.deltaTime;
            ppVolume.weight = exposureWeightHallway;

            if (SceneManager.GetActiveScene().name == "HallwayLevel" && exposureWeightHallway >= 1)
            {
                SceneManager.LoadScene("NurseryLevel");
            }
        }

        if (startNurseryEndFade)
        {
            exposureWeightNursery += Time.deltaTime;
            ppVolume.weight = exposureWeightNursery;

            if (exposureWeightAttic >= 1 && onUITitle != null)
            {
                this.enabled = false;
                onUITitle();
            }
        }
    }

    public void OnFadeOutReceived()
    {
        startFading = true;
    }

    public void OnGameEndFadeOutReceived()
    {
        startHallwayEndFade = true;
    }
}
