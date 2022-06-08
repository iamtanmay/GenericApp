using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRApplication
{
    public class Scenario : MonoBehaviour
    {
        public int ID = -1;

        public Objective[] objectives;

        Dictionary<int, Objective> _objectives;

        public int[] prerequisiteQuests;

        public bool achieved = false;

        public VRApp app;

        public void InitialiseScenario()
        {
        }

        public void ClearScenario()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        public void OnObjectiveAchieved(int ID)
        {
        }

        public void OnObjectiveFailed(int ID)
        {
        }
    }
}