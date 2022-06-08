using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class BunsenBurner : Tool
    {
        public int control_level = 0;
        public float temperature = 273f, RoomTemperature = 273f, FireTemperature1 = 723f, FireTemperature2 = 773f, FireTemperature3 = 823f;
        public Material mat;
        public Color fire1Color, fire2Color, fire3Color;
        public Animation anim;

        public AudioSource burner, clicking;

        public override int Slot { get { return -1; } }

        public override void Init()
        {
        }

        public override void Equip()
        {
        }

        public override void Unequip()
        {
        }

        public override void OnClick(MixedRealityPointerEventData eventData)
        {
            control_level++;

            if (control_level > 3)
            {
                control_level = 0;
            }

            switch (control_level)
            {
                case 0: temperature = RoomTemperature; triggerOnToolDrop.Invoke(); anim.Play("Fire0"); mat.SetColor("_EmissionColor", fire1Color); burner.volume = 0f; break;
                case 1: temperature = FireTemperature1; triggerOnToolPickup.Invoke(); anim.Play("Fire1"); clicking.Play(); burner.volume = 0.5f; break;
                case 2: temperature = FireTemperature2;  anim.Play("Fire2"); mat.SetColor("_EmissionColor", fire2Color); burner.volume = 0.75f; break;
                case 3: temperature = FireTemperature3;  anim.Play("Fire3"); mat.SetColor("_EmissionColor", fire3Color); burner.volume = 1f; break;
                default: break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MagnesiumStrip strip;

            bool exists = other.transform.TryGetComponent<MagnesiumStrip>(out strip);

            if (exists)
                strip.environmentTemperature = temperature;
        }

        void OnTriggerExit(Collider other)
        {
            MagnesiumStrip strip;

            bool exists = other.transform.TryGetComponent<MagnesiumStrip>(out strip);

            if (exists)
                strip.environmentTemperature = RoomTemperature;
        }

        public override void TriggerTool()
        {
        }

        public override void TriggerRelease()
        {
        }
    }
}
