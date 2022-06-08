using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.Events;

namespace VRApplication {
    public abstract class Tool : MonoBehaviour
    {
        public Player player;

        public int _slot;
        
        abstract public int Slot { get; }

        public bool attachedObject = false;

        public bool isEquipped;

        public bool isInit = false;

        public UnityEvent triggerOnToolPickup;
        public UnityEvent triggerOnToolDrop;

        public void Start()
        {
            player = Camera.main.GetComponent<Player>();
            Init();
            isInit = true;
        }

        public void Awake()
        {
            if (!isInit)
            {
                player = Camera.main.GetComponent<Player>();
                Init();
                isInit = true;
            }
        }

        public void TriggerEquip()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }
            player.Equip(this);

            triggerOnToolPickup.Invoke();
        }

        public void TriggerUnequip()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }
            player.Unequip(Slot);
        }

        public abstract void Init();

        public abstract void Equip();

        public abstract void Unequip();

        public abstract void OnClick(MixedRealityPointerEventData eventData);

        public abstract void TriggerTool();

        public abstract void TriggerRelease();
    }
}