using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendPlank : MonoBehaviour
{
    private Animator anim;


    void OnEnable()
    {
        PlayerControlTest.onObjectShakePlank += ShakePlankEventReceived;

        PlayerControls.onObjectShakePlank += ShakePlankEventReceived;
        PlayerControls.onObjectStillPlank += StillPlankEventReceived;
        PlayerControls.onObjectBendPlank += BendPlankEvenetReceived;
    }

    void OnDisable()
    {
        PlayerControlTest.onObjectShakePlank -= ShakePlankEventReceived;

        PlayerControls.onObjectShakePlank += ShakePlankEventReceived;
        PlayerControls.onObjectStillPlank -= StillPlankEventReceived;
        PlayerControls.onObjectBendPlank -= BendPlankEvenetReceived;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void ShakePlankEventReceived()
    {
        anim.Play("LoosePlank");
        Debug.LogWarning("Playing Loose Plank Animation");
    }

    void StillPlankEventReceived()
    {
        anim.Play("StillPlank");
        Debug.LogWarning("Playing Still Plank Animation");
    }

    void BendPlankEvenetReceived()
    {
        // anim.SetBool("isBending", true);
        anim.Play("BendPlank");
        Destroy(GetComponent<SphereCollider>());
        Debug.LogWarning("Playing Bend Plank Animation");
    }
}
