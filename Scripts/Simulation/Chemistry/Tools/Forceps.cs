using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class Forceps : Tool
    {
        public bool open = false;

        public float speed = 1f;

        public Animation anim;

        public override int Slot { get { return 1; } }

        public AudioSource pickup;

        public override void Init()
        {
            open = false;
            anim.Play("forceps_open");
        }

        public override void Equip()
        {
            transform.gameObject.SetActive(false);
            isEquipped = true;
            pickup.Play();
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
            hand.equipped_tool = 1;
            hand.Equip();
            triggerOnToolPickup.Invoke();
        }

        public override void TriggerTool()
        {
            anim.Play("forceps_close");
        }

        public override void TriggerRelease()
        {
            anim.Play("forceps_open");
        }
    }
}