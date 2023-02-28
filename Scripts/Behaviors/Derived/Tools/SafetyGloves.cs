using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class SafetyGloves : Item
    {
        public override int Slot { get { return 1; } }

        public AudioSource pickup;

        public override void Equip(PlayerHand hand)
        {
            pickup.Play();
            hand.isgloved = true;
            transform.gameObject.SetActive(false);
            isEquipped = true;
        }

        public override void Unequip()
        {
            API.updateQuestEvent(typeID, instanceID, questID, false);
            transform.gameObject.SetActive(true);
            isEquipped = false;
        }

        ///// <summary>
        ///// Generic VR Controller Interaction event with eventType for getting source 
        ///// </summary>
        ///// <param name="controller"></param>
        //public override void Interact(IMixedRealityController controller, string eventType)
        //{
        //    //Try to set the flag
        //    API.updateQuestEvent(typeID, instanceID, questID, true);

        //    //If flag is set to true, proceed, otherwise not allowed to activate
        //    if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
        //    {
        //        Debug.Log("Equipping " + typeID + " " + instanceID + " in Quest " + questID);
        //        PlayerHand hand = controller.Visualizer.GameObjectProxy.GetComponent<PlayerHand>();
        //        hand.isgloved = true;
        //        hand.Equip();
        //        Equip();
        //    }
        //}
    }
}