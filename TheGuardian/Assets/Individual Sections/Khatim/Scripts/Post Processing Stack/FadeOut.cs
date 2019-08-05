using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public float exposureWeightAttic;
    public float exposureWeightHallway;
    public bool startFading;
    public bool startEndFading;
    private PostProcessVolume ppVolume;

    void OnEnable()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        EventManager.onAtticFadeOut += OnFadeOutReceived;
        PlayerControls.onHallwayFadeout += OnFadeOutReceived;
    }

    void OnDisable()
    {
        EventManager.onAtticFadeOut -= OnFadeOutReceived;
        PlayerControls.onHallwayFadeout -= OnFadeOutReceived;
    }

    void OnDestroy()
    {
        EventManager.onAtticFadeOut -= OnFadeOutReceived;
        PlayerControls.onHallwayFadeout -= OnFadeOutReceived;
    }

    void Update()
    {
        if (startFading)
        {
            exposureWeightAttic += Time.deltaTime;
            ppVolume.weight = exposureWeightAttic;

            if (exposureWeightAttic >= 1)
                this.enabled = false;
        }

        if (startEndFading)
        {
            exposureWeightHallway += Time.deltaTime;
            ppVolume.weight = exposureWeightHallway;

            if (SceneManager.GetActiveScene().name == "HallwayLevel" && exposureWeightHallway >= 1)
            {
                SceneManager.LoadScene("NurseryLevel");
            }
        }
    }

    public void OnFadeOutReceived()
    {
        startFading = true;
    }

    public void OnGameEndFadeOutReceived()
    {
        startEndFading = true;
    }
}
