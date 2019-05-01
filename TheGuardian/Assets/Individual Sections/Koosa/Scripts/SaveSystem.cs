using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public  class SaveSystem : MonoBehaviour
{
    static void SaveCharacter(GameObject data)
    {
        PlayerPrefs.SetFloat("vector1player" ,data.transform.position.x);
        PlayerPrefs.SetFloat("vector2player" , data.transform.position.y);
        PlayerPrefs.SetFloat("vector3player" , data.transform.position.z);
    }

    static void SaveKeyPosition(GameObject data)
    {
        PlayerPrefs.SetFloat("vector1key", data.transform.position.x);
        PlayerPrefs.SetFloat("vector2key", data.transform.position.y);
        PlayerPrefs.SetFloat("vector3key", data.transform.position.z);
    }

    static void Loadharacter(GameObject data)
    {
        PlayerPrefs.SetFloat("vector1player", data.transform.position.x);
        PlayerPrefs.SetFloat("vector2player", data.transform.position.y);
        PlayerPrefs.SetFloat("vector3player", data.transform.position.z);
    }

    static void LoadKeyPosition(GameObject data)
    {
        PlayerPrefs.SetFloat("vector1key", data.transform.position.x);
        PlayerPrefs.SetFloat("vector2key", data.transform.position.y);
        PlayerPrefs.SetFloat("vector3key", data.transform.position.z);
    }
}
	

