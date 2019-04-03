using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{

    public override void Execute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute();
            if (children[i].state == Node_State.success)
            {
                state = Node_State.success;
                return;
            }
            if (children[i].state == Node_State.running)
            {
                state = Node_State.running;
                return;

            }
        }
        state = Node_State.faliure;
    }
}
