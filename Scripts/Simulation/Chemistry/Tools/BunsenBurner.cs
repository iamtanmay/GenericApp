using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class BunsenBurner : Tool
    {
        public int control_level = 0;

        public bool simpleMode = true;

        public float temperature = 273f, RoomTemperature = 273f, FireTemperature1 = 723f, FireTemperature2 = 773f, FireTemperature3 = 823f;

        public Material mat;

        public Color fire1Color, fire2Color, fire3Color;

        public AudioSource burner, clicking;

        public override int Slot { get { return -1; } }

        /// <summary>
        /// Generic VR Controller Interaction event with eventType for getting source 
        /// </summary>
        /// <param name="controller"></param>
        public override void InteractVR(IMixedRealityController controller, string eventType)
        {
            Activate();
        }

        public override void Activate()
        {
            //Try to set the flag
            API.updateQuestEvent(typeID, instanceID, questID, true);

            //If flag is set to true, proceed, otherwise not allowed to activate
            if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
            {
                control_level++;

                //Simple mode prevents turning off burner
                if (simpleMode && control_level > 2)
                    return;

                if (control_level > 3)
                    control_level = 0;

                switch (control_level)
                {
                    case 0: temperature = RoomTemperature; API.updateQuestEvent(typeID, instanceID, questID, false); anim.Play("Fire0"); mat.SetColor("_EmissionColor", fire1Color); burner.volume = 0f; break;
                    case 1: temperature = FireTemperature1; API.updateQuestEvent(typeID, instanceID, questID, true); anim.Play("Fire1"); clicking.Play(); burner.volume = 0.5f; break;
                    case 2: temperature = FireTemperature2; anim.Play("Fire2"); mat.SetColor("_EmissionColor", fire2Color); burner.volume = 0.75f; break;
                    case 3: temperature = FireTemperature3; anim.Play("Fire3"); mat.SetColor("_EmissionColor", fire3Color); burner.volume = 1f; break;
                    default: break;
                }

                //Simple mode transition through flame levels
                if (simpleMode)
                    if (control_level == 1 || control_level == 2)
                        Activate();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Tool tool;

            bool exists = other.transform.TryGetComponent<Tool>(out tool);

            if (exists)
                tool.Heat(temperature);
        }

        void OnTriggerExit(Collider other)
        {
            Tool tool;

            bool exists = other.transform.TryGetComponent<Tool>(out tool);

            if (exists)
                tool.Heat(API.RoomTemperature);
        }
    }
}