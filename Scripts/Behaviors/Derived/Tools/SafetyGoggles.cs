using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class SafetyGoggles : Tool
    {
        public GameObject CameraOverlay;

        public Vector3 InitialPos, InitialRot;

        public override int Slot { get { return 0; } }

        public override void Init()
        {
            InitialPos = transform.position;
            InitialRot = transform.rotation.eulerAngles;
        }

        public override void Equip(PlayerHand hand)
        {
            if (CameraOverlay == null)
            {
                Transform t = player.transform.Find("Glasses");
                CameraOverlay = t.gameObject;
            }

            //Try to set the flag
            API.updateQuestEvent(typeID, instanceID, questID, true);

            //If flag is set to true, proceed, otherwise not allowed to activate
            if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
            {
                CameraOverlay.SetActive(true);
                transform.position = InitialPos;
                transform.rotation = Quaternion.Euler(InitialRot);
                transform.gameObject.SetActive(false);
                isEquipped = true;
                triggerOnToolPickup.Invoke();
            }
        }

        public override void Unequip()
        {
            API.updateQuestEvent(typeID, instanceID, questID, false);
            CameraOverlay.SetActive(false);
            transform.gameObject.SetActive(true);
            isEquipped = false;
        }
    }
}