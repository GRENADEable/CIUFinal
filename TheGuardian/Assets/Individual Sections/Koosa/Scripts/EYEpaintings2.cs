using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EYEpaintings2 : MonoBehaviour
{
    public float maxAngleY;
    public float maxAngleX;
    public float maxAngleZ;
    public float speed;

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
    
    public void LightRotator( float delta)
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
                if (transform.rotation.eulerAngles.y >= 30 && transform.rotation.eulerAngles.y <= 32)
                {
                    rotationInt++;
                    transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                    Debug.Log("doing the first rotation");
                    yield return new WaitForSeconds(2f);
                    // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                }
                    if (transform.rotation.eulerAngles.y >= 0 && transform.rotation.eulerAngles.y <= 2)
                    {
                        rotationInt++;
                        transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                        Debug.Log("doing the second rotation");
                        yield return new WaitForSeconds(2f);
                        // transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                    }
                        if (transform.rotation.eulerAngles.y >= -35 && transform.rotation.eulerAngles.y <= -33)
                        {
                            rotationInt++;
                             transform.rotation = Quaternion.Euler(new Vector3(maxAngleX, delta * maxAngleY, maxAngleZ));
                             Debug.Log("doing the third rotation");
                            yield return new WaitForSeconds(2f);
                        }
            yield return null;
        }
    }
}
