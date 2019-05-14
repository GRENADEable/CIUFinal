using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public float exposureWeight;
    public bool startFading;
    private PostProcessVolume ppVolume;

    void OnEnable()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        PlayerControls.onFadeOut += OnFadeOutReceived;
    }

    void Update()
    {
        if (startFading)
        {
            exposureWeight += Time.deltaTime;
            ppVolume.weight = exposureWeight;

            if (SceneManager.GetActiveScene().name == "HallwayLevel" && exposureWeight >= 1)
            {
                SceneManager.LoadScene("NurseryLevel");
            }
        }
    }

    public void OnFadeOutReceived()
    {
        startFading = true;
    }
}
