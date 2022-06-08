using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRApplication
{
    public class Player : MonoBehaviour
    {
        //Face, gloves, left hand, right hand
        public bool[] Slots = new bool[] { false, false, false, false };

        public List<Tool> EquippedTools = new List<Tool>();

        DummyTool _dummyTool;

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            _dummyTool = transform.GetComponent<DummyTool>();

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
                    EquippedTools[newitemslot].Equip();
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
            if (obj.TryGetComponent<Tool>(out newTool))
                if (newTool.Slot == 0)
                    newTool.TriggerEquip();
        }
    }
}
