using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Node_State { running, success, faliure };

public class Node
{
    public BehaviourTree BT;
    public List<Node> children = new List<Node>();
    public bool init;
    public Node headnode;
    public object value;
    public Node_State state;
    public bool process;




    public void Start()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].BT = BT;
            children[i].Start();
        }

        if (state == Node_State.running)
        {
            // Debug.Log("CURRENT STATE = RUNNING");
        }
    }
    public virtual void Execute()
    {
        init = true;
        Debug.Log("I am  initialised");
    }
}









