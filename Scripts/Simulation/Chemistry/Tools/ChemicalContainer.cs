using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class ChemicalContainer : Tool
    {
        public Color liquidColor;

        public string contents;

        public float temperature = 273f, roomTemperature = 273f;

        public Material mat;

        public AudioSource burner, clicking;

        public override int Slot { get { return -1; } }

        public override void Init()
        {
        }

        public override void Activate()
        {
            //Try to set the flag
            API.updateQuestEvent(typeID, instanceID, questID, true);

            //If flag is set to true, proceed, otherwise not allowed to activate
            if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
            {
                //case 0: temperature = roomTemperature; liquidColor; API.updateQuestEvent(typeID, instanceID, questID, false); anim.Play("Fire0"); mat.SetColor("_EmissionColor", fire1Color); burner.volume = 0f; break;
            }
        }

        public override void Activate(Tool tool)
        {
        }

        public override void Release()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            Ingredient ingredient;

            bool exists = other.transform.TryGetComponent<Ingredient>(out ingredient);

            if (exists)
            {
            }

            Tool tool;

            exists = other.transform.TryGetComponent<Tool>(out tool);

            if (exists)
                tool.Activate(this);
        }

        void OnTriggerExit(Collider other)
        {
        }
    }
}