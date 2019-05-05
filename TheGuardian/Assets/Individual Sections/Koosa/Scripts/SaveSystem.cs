using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public  class SaveSystem : MonoBehaviour
{ 
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SaveCharacter(GameObject data, int slot)
    {
        PlayerPrefs.SetFloat("vector1player"+ slot ,data.transform.position.x);
        PlayerPrefs.SetFloat("vector2player" + slot, data.transform.position.y);
        PlayerPrefs.SetFloat("vector3player" + slot, data.transform.position.z);
    }

    public void SaveKeyPosition(GameObject data, int slot)
    {
        PlayerPrefs.SetFloat("vector1key" + slot, data.transform.position.x);
        PlayerPrefs.SetFloat("vector2key" + slot, data.transform.position.y);
        PlayerPrefs.SetFloat("vector3key" + slot, data.transform.position.z);
    }

    public Vector3 Loadharacter(int slot)
    {
        float x  =PlayerPrefs.GetFloat("vector1player" + slot);
        float y = PlayerPrefs.GetFloat("vector2player" + slot);
        float z = PlayerPrefs.GetFloat("vector3player" + slot);
        Vector3 position = new Vector3(x, y, z);
        return position;
    }

    public Vector3 LoadKeyPosition(int slot)
    {
        float x = PlayerPrefs.GetFloat("vector1key" + slot);
        float y = PlayerPrefs.GetFloat("vector2key" + slot);
        float z = PlayerPrefs.GetFloat("vector3key" + slot);
        Vector3 position = new Vector3(x, y, z);
        return position;
    }
}
	

