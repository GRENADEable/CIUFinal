using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendPlank : MonoBehaviour
{
    private Animator anim;


    void OnEnable()
    {
        PlayerControlTest.onObjectBendPlank += BendPlankEventReceived;

    }

    void OnDisable()
    {
        PlayerControlTest.onObjectBendPlank -= BendPlankEventReceived;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void BendPlankEventReceived()
    {
        anim.SetInteger("bendingValue", 2);
        Debug.LogWarning("Plank Swinging");
    }
}
