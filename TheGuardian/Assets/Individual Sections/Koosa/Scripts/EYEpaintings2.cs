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

    public void Start()
    {
        StartCoroutine(lighting());
    }

    IEnumerator lighting()
    {
        while (true)
        {
            int rotationInt = 0;
            time = time + Time.deltaTime;
            delta = Mathf.Sin(time / speed);
            transform.localRotation = Quaternion.Euler(maxAngleX, delta * maxAngleY, maxAngleZ);
            Debug.Log(rotationInt);
            // Debug.Log(transform.eulerAngles.y);
            if ((transform.rotation.eulerAngles.y >= firstRotationSmaller && transform.rotation.eulerAngles.y <= firstRotationBigger) || (transform.rotation.eulerAngles.y >= firstRotationBigger && transform.rotation.eulerAngles.y <= firstRotationSmaller))
            {
                rotationInt++;
                // transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, delta * maxAngleY, transform.eulerAngles.z));
                Debug.Log("doing the first rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
                // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
            }
            if ((transform.rotation.eulerAngles.y >= secondRotationSmaller && transform.rotation.eulerAngles.y <= secondRotationBigger) || (transform.rotation.eulerAngles.y >= secondRotationBigger && transform.rotation.eulerAngles.y <= secondRotationSmaller))
            {
                rotationInt++;
                // transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, delta * maxAngleY, transform.eulerAngles.z));
                Debug.Log("doing the second rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
                // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
            }
            if ((transform.rotation.eulerAngles.y >= thirdRotationSmaller && transform.rotation.eulerAngles.y <= thirdRotationBigger) || (transform.rotation.eulerAngles.y >= thirdRotationBigger && transform.rotation.eulerAngles.y <= thirdRotationSmaller))
            {
                rotationInt++;
                // transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, delta * maxAngleY, transform.eulerAngles.z));
                Debug.Log("doing the third rotation");
                yield return new WaitForSeconds(timeToWaitBetweenRotations);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
