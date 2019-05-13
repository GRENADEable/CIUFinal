using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FadeIn : MonoBehaviour
{
    public float exposureWeight;
    private PostProcessVolume ppVolume;

    void Start()
    {
        ppVolume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        exposureWeight -= Time.deltaTime;
        ppVolume.weight = exposureWeight;
    }
}
