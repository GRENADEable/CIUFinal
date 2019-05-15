using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReset : MonoBehaviour
{
    public float height;
    [SerializeField]
    private ItemData[] items;
    [System.Serializable]
    private struct ItemData
    {
        public Rigidbody item;
        [System.NonSerialized]
        public Vector3 startPosition;
        [System.NonSerialized]
        public Quaternion startRotation;
    }

    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].startPosition = items[i].item.position;
            items[i].startRotation = items[i].item.rotation;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item.position.y <= height)
            {
                items[i].item.position = items[i].startPosition;
                items[i].item.rotation = items[i].startRotation;
                items[i].item.velocity = Vector3.zero;
                items[i].item.angularVelocity = Vector3.zero;
            }
        }
    }
}
