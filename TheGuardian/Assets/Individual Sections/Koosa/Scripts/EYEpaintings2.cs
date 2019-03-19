using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EYEpaintings2 : MonoBehaviour
{
    public float maxAngle;
    public float speed;

    private float time;
    private float delta;

    public void Update()
    {
        TimeChanger();
        LightRotator(delta);
    }

    public void TimeChanger()
    {
        time = time + Time.deltaTime;
        delta = Mathf.Sin(time / speed);
    }
    
    public void LightRotator( float delta)
    {
       transform.rotation = Quaternion.Euler(new Vector3(delta * maxAngle, transform.rotation.y, transform.rotation.z));
    }
}
