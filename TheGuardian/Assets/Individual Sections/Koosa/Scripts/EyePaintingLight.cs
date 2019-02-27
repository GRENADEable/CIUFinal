using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePaintingLight : MonoBehaviour
{

   // public float nextActionTime = 0.0f;
   // public float fTimeForChange = 0.1f;
   // public int amountOfTimesAngleChanged;
    public float rotation1;
    public float rotation2;
    public float rotation3;
    public float timeToWait;

    void Start()
    {
        StartCoroutine(LightChange());
    }

    IEnumerator LightChange()
    {
        while (true)
        {
            transform.rotation = Quaternion.Euler(rotation1, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
            transform.rotation = Quaternion.Euler(rotation2, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
            transform.rotation = Quaternion.Euler(rotation3, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
        }
    }
}
