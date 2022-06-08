using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class SafetyGloves : Tool
    {
        public override int Slot { get { return 1; } }

        public AudioSource pickup;

        public override void Init()
        {
        }

        public override void Equip()
        {
            transform.gameObject.SetActive(false);
            isEquipped = true;
        }

        public override void Unequip()
        {
            transform.gameObject.SetActive(true);
            isEquipped = false;
        }

        public override void OnClick(MixedRealityPointerEventData eventData)
        {
            IMixedRealityController controller = eventData.Pointer.Controller;
            PlayerHand hand = controller.Visualizer.GameObjectProxy.GetComponent<PlayerHand>();
            hand.isgloved = true;            
            hand.Equip();
            triggerOnToolPickup.Invoke();
            pickup.Play();
        }

        public override void TriggerTool()
        {
        }

        public override void TriggerRelease()
        {
        }
    }
}