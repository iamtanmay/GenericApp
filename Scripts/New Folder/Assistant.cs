using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRApplication
{
    public class Assistant : MonoBehaviour
    {
        public Objective[] objectives;
        public bool success = false, _countdown_started = false;
        public int _countdown_timer = 600;
        public AudioSource success1, success2;
        public Transform fX;

        public void Update()
        {
            if (_countdown_started)
            {
                _countdown_timer--;

                if (_countdown_timer < 1)
                {
                    fX.gameObject.SetActive(false);
                    this.enabled = false;
                }
            }
            else
            {
                if (!success)
                {
                    success = true;
                    for (int i = 0; i < objectives.Length; i++)
                    {
                        if (!objectives[i].achieved)
                            success = false;
                    }
                }
                else
                {
                    success1.Play();
                    success2.Play();
                    fX.gameObject.SetActive(true);

                    for (int i = 0; i < objectives.Length; i++)
                    {
                        objectives[i].enabled = false;
                    }
                    _countdown_started = true;
                }
            }
        }

        public VRApp app;

        public GameObject[] menus;

        public void CloseMenus()
        {            
            foreach (GameObject item in menus)
                item.SetActive(false);
        }

        public void OpenMainMenu()
        {      
            Debug.Log("Main Menu");      
            CloseMenus();
            menus[0].SetActive(true);
        }

        public void OpenScenarioSelectionMenu()
        {            
            Debug.Log("Scenarios Menu");      
            CloseMenus();
            menus[1].SetActive(true);
        }

        public void OpenSettingsMenu()
        {            
            Debug.Log("Settings Menu");      
            CloseMenus();
            //menus[2].SetActive(true);
        }

        public void ExitApp()
        {    
            Debug.Log("Exit App");      
            CloseMenus();
            Application.Quit();
        }

        public void GreetUser(int scenarioID)
        {
        }

        public void StartScenario(int scenarioID)
        {
            Debug.Log("Start Scenario " + scenarioID);
        }

        public void ReviewScenario(int scenarioID)
        {
            Debug.Log("Review Scenario " + scenarioID);
        }

        public void ExitScenario()
        {
        }
    }
}