using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Type", fileName = "New Action Type")]
public class ActionType : ScriptableObject
{
    public Action prefab;
    public string description;
}