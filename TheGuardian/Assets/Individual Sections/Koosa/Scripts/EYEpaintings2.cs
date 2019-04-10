using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EYEpaintings2 : MonoBehaviour
{
    public float maxAngleY;
    public float maxAngleX;
    public float maxAngleZ;
    public float speed;
    public float timeToWaitBetweenRotations;

    public float firstRotationSmaller; // smaller value than the the bigger value 
    public float firstRotationBigger;
    public float secondRotationSmaller;
    public float secondRotationBigger;
    public float thirdRotationSmaller;
    public float thirdRotationBigger;

    private float time;
    private float delta;

    public void Update()
    {
        // TimeChanger();
        // LightRotator(delta);
    }
    public void Start()
    {
        StartCoroutine(lighting());
    }

    public void TimeChanger()
    {
        time = time + Time.deltaTime;
        delta = Mathf.Sin(time / speed);
    }

    public void LightRotator(float delta)
    {
        transform.rotation = Quaternion.Euler(new Vector3(delta * maxAngleX, maxAngleY, maxAngleZ));
    }

    IEnumerator lighting()
    {
        while (true)
        {
            int rotationInt = 0;
            time = time + Time.deltaTime;
            delta = Mathf.Sin(time / speed);
            transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
            Debug.Log(rotationInt);
            if (transform.rotation.eulerAngles.y >= firstRotationSmaller && transform.rotation.eulerAngles.y <= firstRotationBigger)
            {
                rotationInt++;
                transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                Debug.Log("doing the first rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
                // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
            }
            if (transform.rotation.eulerAngles.y >= secondRotationSmaller && transform.rotation.eulerAngles.y <= secondRotationBigger)
            {
                rotationInt++;
                transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                Debug.Log("doing the second rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
                // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
            }
            if (transform.rotation.eulerAngles.y >= thirdRotationSmaller && transform.rotation.eulerAngles.y <= thirdRotationBigger)
            {
                rotationInt++;
                transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                Debug.Log("doing the third rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
