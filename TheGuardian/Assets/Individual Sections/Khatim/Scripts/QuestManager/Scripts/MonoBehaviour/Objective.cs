using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ObjectiveType
{
    every,
    any
}
public class Objective : MonoBehaviour
{
    private Quest quest;
    public string description;
    public ObjectiveType type;

    [Space]
    public UnityEvent onBegin;
    public UnityEvent onComplete;
    [Space]
    public List<GameObject> enabledOnBegin;
    public List<GameObject> disabledOnBegin;
    [Space]
    public List<GameObject> enabledOnComplete;
    public List<GameObject> disabledOnComplete;
    [Space]
    [HideInInspector]
    public List<Action> actions;

    public Quest Quest
    {
        set
        {
            quest = value;
        }
    }
    private void Awake()
    {
        if (quest == null)
        {
            quest = this.GetComponentInParent<Quest>();
        }
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i])
                actions[i].gameObject.SetActive(false);
        }
    }
    public void Begin()
    {
        onBegin.Invoke();
        for (int i = 0; i < enabledOnBegin.Count; i++)
        {
            if (enabledOnBegin[i])
                enabledOnBegin[i].SetActive(true);

        }
        for (int i = 0; i < disabledOnBegin.Count; i++)
        {
            if (disabledOnBegin[i])

                disabledOnBegin[i].SetActive(false);
        }
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].gameObject.SetActive(true);
            actions[i].Begin();
        }
    }

    public void End(Action action)
    {
        actions.Remove(action);
        action.gameObject.SetActive(false);
        if (ObjectiveType.any == type)
            EndObjective();
        else
        {
            if (actions.Count == 0)
            {
                if (quest.currentObjective == this)
                    EndObjective();
            }
        }
    }

    private void EndObjective()
    {
        if (!quest)
            quest = GetComponentInParent<Quest>();

        if (quest)
            quest.Next();
        else
            Complete();
    }

    public virtual void Complete()
    {
        onComplete.Invoke();
        for (int i = 0; i < enabledOnComplete.Count; i++)
        {
            if (enabledOnComplete[i])
                enabledOnComplete[i].SetActive(true);
        }
        for (int i = 0; i < disabledOnComplete.Count; i++)
        {
            if (disabledOnComplete[i])
                disabledOnComplete[i].SetActive(false);
        }
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
    }
}