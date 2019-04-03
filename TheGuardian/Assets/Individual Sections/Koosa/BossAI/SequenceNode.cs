using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public override void Execute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute();
            if (children[i].state == Node_State.faliure)
            {
                state = Node_State.faliure;
                return;
            }
            if (children[i].state == Node_State.running)
            {
                state = Node_State.running;
                return;
            }
        }
        state = Node_State.success;
    }
}


