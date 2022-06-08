using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRApplication
{
    /// <summary>
    /// Manages startup and config of application
    /// </summary>
    public class VRApp : MonoBehaviour
    {
        public int CurrentUser = -1;
        public int CurrentScenario = -1;

        GameObject _menu;
        Assistant _assistant;
        Simulation _simulation;
        User[] _users;
        Scenario[] _scenarios;
        Dictionary<string, Scenario> _scenarioDict;

        public void Start()
        {
            //Start Main Menu
            _menu.SetActive(true);
        }

        public void MoveToLocation(Transform itransform)
        {
        }

        public void AddScenario(Scenario iScenario)
        {
        }

        public void StartScenario(int iScenario)
        {
            //Disable Start Menu
            _menu.SetActive(false);
        }

        public void EndScenario()
        {
            //Start Main Menu
            _menu.SetActive(true);
        }
    }
}