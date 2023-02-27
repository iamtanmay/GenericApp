using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class Player : MonoBehaviour
    {
        //Face, gloves, left hand, right hand
        public bool[] Slots = new bool[] { false, false, false, false };

        public bool handRayEnabled = false;

        public PlayerHand leftHand, rightHand;

        public List<Tool> EquippedTools = new List<Tool>();

        DummyTool _dummyTool;

        public void Start()
        {
            Init();

            if (!handRayEnabled)
                PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
        }

        public void initHands()
        {
            PlayerHand[] hands = transform.parent.GetComponentsInChildren<PlayerHand>();

            if (hands.Length > 0)
            {
                if (hands[0].righthand)
                    rightHand = hands[0];
                else
                    leftHand = hands[0];
            }
            if (hands.Length > 1)
            {
                if (hands[1].righthand)
                    rightHand = hands[1];
                else
                    leftHand = hands[1];
            }
        }

        public void Init()
        {
            _dummyTool = transform.GetComponent<DummyTool>();

            initHands();

            for (int i=0; i< Slots.Length; i++)
                EquippedTools.Add(_dummyTool);
        }

        public void Equip(Tool itool)
        {
            int newitemslot = itool.Slot;

            if (newitemslot < Slots.Length-1)
            {
                if (!Slots[newitemslot])
                {
                    EquippedTools[newitemslot] = itool;
                    //PlayerHand is null here because only collider triggered tools equip here
                    EquippedTools[newitemslot].Equip(null);
                    Slots[newitemslot] = true;
                }
                else {
                    Unequip(newitemslot);
                }
            }
        }

        public void Unequip(int islot)
        {
            if (islot < Slots.Length - 1)
            {
                if (Slots[islot])
                {
                    EquippedTools[islot].Unequip();
                    EquippedTools[islot] = _dummyTool;
                    Slots[islot] = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Transform obj = other.transform;
            Tool newTool;

            if (obj.TryGetComponent<Tool>(out newTool) || obj.parent.TryGetComponent<Tool>(out newTool))
                if (newTool.Slot == 0)
                    newTool.TriggerEquip();
        }
    }
}
