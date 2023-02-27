using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AppStarter;

public class Objectives
{
    public List<string> flagNames = new List<string>();
    public List<bool> flagValues = new List<bool>();
    public GameObject quest;
    public string questID;
    public int objectiveCounter = 0;
    public int currObjective = 0;
    //Can objectives be succeeded in linear order only or not
    public bool areObjectivesLinear = false;
    public API API;

    public Objectives()
    {
        API[] temp = Object.FindObjectsOfType<API>();
        API = temp[0];
    }

    public void Initialise(GameObject iquest, bool linear, List<string> iflags)
    {
        quest = iquest;
        areObjectivesLinear = linear;
        Debug.Log("Quest is linear: " + areObjectivesLinear);
        CreateFlags(iflags);
    }

    public bool ExistsFlag(string iKey)
    {
        return flagNames.Contains(iKey);
    }

    public bool GetFlag(string iKey)
    {
        if (ExistsFlag(iKey))
        {
            int index = flagNames.FindIndex(a => a == iKey);
            return flagValues[index];
        }
        return false;
    }

    public void SetFlag(string iKey, bool iflag)
    {
        if (ExistsFlag(iKey))
        {
            int index = flagNames.FindIndex(a => a==iKey);
            SetFlag(index, iflag);
        }
    }

    public void SetFlag(int index, bool iflag)
    {
        Debug.Log("Setting Flag " + index + " to " + iflag);
        flagValues[index] = iflag;
        CustomEvent.Trigger(quest, "updateJournal", index, iflag);
    }

    public void CreateFlags(List<string> iflags)
    {
        flagNames = iflags;
        flagValues = new List<bool>();

        for (int i = 0; i < iflags.Count; i++)
            flagValues.Add(false);
    }

    public void CountSuccessFlags()
    {
        objectiveCounter = 0;

        for (int i = 0; i < flagValues.Count; i++)
            if (flagValues[i])
                objectiveCounter++;
    }

    public bool UpdateObjectives(string ievent, bool iflag, string iID)
    {
        Debug.Log("Event Triggered " + ievent + " " + iflag + " " + iID);
        if (flagNames.Contains(ievent))
        {
            //False flag can always be set
            if (!iflag)
            {
                SetFlag(ievent, iflag);
                CountSuccessFlags();
                return false;
            }
            else
            {
                if (areObjectivesLinear)
                {
                    //Check to see if previous flags are true
                    int index = flagNames.FindIndex(a => a == ievent);                    

                    for (int i = 0; i < index; i++)
                        if (!flagValues[i])
                        {
                            //Set subsequent flags to false as well
                            for (int j = i+1; j <= index; j++)
                                SetFlag(j, false);
                            return false;
                        }

                    SetFlag(index, iflag);
                    Debug.Log(index + " " + iflag + " " + currObjective);
                    if (iflag == true && index >= currObjective)
                    {
                        currObjective++;
                        API.updateCurrentPriority(currObjective);
                    }
                }
                else
                {
                    SetFlag(ievent, iflag);
                }

                CountSuccessFlags();

                if (objectiveCounter == flagValues.Count)
                    return true;
            }
        }
        return false;
    }
}