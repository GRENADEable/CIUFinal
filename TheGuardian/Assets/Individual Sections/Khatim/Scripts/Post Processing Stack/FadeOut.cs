using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

            if (exposureWeight >= 1)
                this.enabled = false;
        }
    }

    void OnFadeOutReceived()
    {
        startFading = true;
    }
}
