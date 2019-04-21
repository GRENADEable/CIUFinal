using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FadeToBlack : MonoBehaviour
{
    public float exposureWeight;
    private PostProcessVolume ppVolume;

    void Start()
    {
        ppVolume = GetComponent<PostProcessVolume>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            exposureWeight += Time.deltaTime;
            ppVolume.weight = exposureWeight;
        }
    }
}
