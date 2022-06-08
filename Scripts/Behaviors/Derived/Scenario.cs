using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRApplication
{
    public class Objective : MonoBehaviour
    {
        public int ID = -1;

        public bool achieved = false;

        public TMPro.TextMeshPro description;

        public string descriptionText = "";

        public bool[] dependencies;

        public int[] prerequisiteObjectives;

        public Scenario quest;

        public AudioSource right, wrong;

        public void OnAchieveDependency()
        {
            description.text = "O";
            right.Play();
            achieved = true;
        }

        public void OnFailDependency()
        {
            description.text = "X";
            wrong.Play();
            achieved = false;
        }

        public bool CheckPrerequisiteObjectives()
        {
            return true;
        }
    }
}