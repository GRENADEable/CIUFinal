using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public Objective objective;
    public UnityEngine.Events.UnityEvent onComplete;

    public virtual void Complete()
    {
        onComplete.Invoke();
        objective.End(this);
    }
    public virtual void Begin()
    {
        this.gameObject.SetActive(true);
    }
}