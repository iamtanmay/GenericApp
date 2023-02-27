using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppStarter
{
    public class Quest : Tool
    {
        public List<bool> objectiveFlags = new List<bool>();

        public List<string> objectiveNames = new List<string>();

        public List<Transform> objectiveTransforms = new List<Transform>();

        public List<string> objectiveContainers = new List<string>();

        public List<string> objectiveSlots = new List<string>();

        public bool isLinear = false;

        public override int Slot { get { return -1; } }

        public void SetupObjectives()
        {
            for (int i = 0; i < objectiveTransforms.Count; i++)
                API.PlaceInContainer(objectiveTransforms[i], objectiveContainers[i], objectiveSlots[i]);
        }

        public bool PreviousObjectiveFlagsComplete(int index)
        {
            for (int i = index; i > -1; i--)
                if (!objectiveFlags[i])
                    return false;
            return true;
        }

        public void UpdateFlags(string iFlagID, bool iFlag)
        {
            int index = objectiveNames.FindIndex(x => x == iFlagID);

            if (index != -1 && index < objectiveFlags.Count)
            {
                if ((isLinear && PreviousObjectiveFlagsComplete(index - 1)) || !isLinear)
                    objectiveFlags[index] = iFlag;

                if (PreviousObjectiveFlagsComplete(objectiveFlags.Count - 1))
                    API.updateQuestEvent(typeID, instanceID, questID, false);
                else
                    API.updateQuestEvent(typeID, instanceID, questID, true);
            }
        }

        public override void Activate()
        {
        }
    }
}