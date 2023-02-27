using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class Dipper : Tool
    {
        public bool burning = false;

        public float burnTemperature = 473f;

        public Material mat;

        public GameObject flameObject;

        public List<string> dipperFlameSubstances = new List<string>();

        public List<Color> flameColors = new List<Color>();

        public override int Slot { get { return 1; } }

        public override void Init()
        {
        }

        public override void Activate()
        {
        }

        public override void Activate(Tool tool)
        {
            //If its a container
            ChemicalContainer chemicalContainer;
            bool exists = tool.transform.TryGetComponent<ChemicalContainer>(out chemicalContainer);

            if (exists)
            {
                contents = chemicalContainer.contents;
                quests = chemicalContainer.quests;
            }
        }

        public override void Release()
        {
        }

        public override void Heat(float temperature)
        {
            if (!burning)
            {
                if (contents != "")
                {
                    int index = dipperFlameSubstances.FindIndex(a => a.Contains(contents));

                    if (index >= 0)
                    {
                        burning = true;
                        flameObject.SetActive(true);

                        if (quests != null)
                            UpdateQuests(contents, true);
                        else
                            API.updateQuestEvent(contents, instanceID, questID, true);
                    }

                    contents = "";
                }
            }
            else
            {
                burning = false;
                flameObject.SetActive(false);
            }
        }
    }
}