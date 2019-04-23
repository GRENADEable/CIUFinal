using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Objective> objectives;
    public Objective currentObjective;
    public int id;
    private int current;

    #region properties
    public bool Ended
    {
        get
        {
            return current < objectives.Count;
        }
    }

    public int Current
    {
        get
        {
            return current;
        }
    }
    #endregion

    #region MonoBehaviour messages
    void Start()
    {
        current = 0;
        currentObjective = objectives[current];
        //currentObjective.Begin();
    }
    #endregion

    #region public methods
    public void Next()
    {
        if (current < objectives.Count)
            objectives[current].Complete();
        current++;
        if (current >= objectives.Count)
        {
            //reward.Activate();
        }
        else
        {
            objectives[current].Begin();
            currentObjective = objectives[current];

        }
    }
    public void Previous()
    {
        objectives[current].Complete();
        current--;
        if (current < 0)
        {
            current = 0;
        }
        objectives[current].Begin();
        currentObjective = objectives[current];
    }
    public void Restart()
    {
        current = 0;
        if (null != objectives && objectives.Count > 0)
            currentObjective = objectives[0];
    }

    public void StartQuet()
    {
        currentObjective.Begin();
    }
    #endregion
}