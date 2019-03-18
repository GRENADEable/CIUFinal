using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsAI : MonoBehaviour
{
    public float maxAngleY;
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

    public void LightRotator(float delta)
    {
        // transform.localRotation = Quaternion.Euler(new Vector3(0, delta * maxAngleY, 0));
        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, delta * maxAngleY, transform.localRotation.z));
    }
}
