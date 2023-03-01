using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.Events;

namespace AppStarter
{
    public enum Tools : int { None = -1, Other = 0, Forceps = 1, Dipper = 2, Dropper_Pipette = 3 }

    public abstract class Item : MonoBehaviour
    {
        public Player player;

        public API API;

        public string typeID, instanceID, questID, contents = "";

        public int _slot;

        abstract public int Slot { get; }

        public float interactRange = 0.15f, speed = 1f;

        public bool attachedObject = false, pickUpAble = false, isEquipped, isHand = false, onlyPickUpWithForceps = false, highlighted = false;

        public AudioSource pickup;

        public Animation anim;

        //Quest Variables
        //Priority level to highlight tool. Priority of current event in quest
        public int priority = -1, currentpriority = 0;

        public Outline outline;

        public bool isInit = false;

        public List<Quest> quests = new List<Quest>();

        public UnityEvent triggerOnToolPickup, triggerOnToolDrop;

        public Rigidbody toolPhysics;

        //Physical Variables
        public float temperature = 300, envTemperature = 300, ignitionPoint = 744, boilingPoint = 744, meltingPoint = 744, smokingPoint = 744;

        public float mass = 0.01f, surfaceArea = 0.004f, specificHeatCapacity = 1017, thermalConductivity = 151, electricalConductivity = 151;

        public void Start()
        {
            player = Camera.main.GetComponent<Player>();
            if (!isInit)
            {
                Init();
                isInit = true;
            }
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

        public void SendEvent(string imessage, bool iflag)
        {

        }

        public void ToggleHighlight(bool toggle)
        {
            if (outline != null)
            {
                if (!highlighted && toggle)
                    outline.enabled = true;
                else if (highlighted && !toggle)
                    outline.enabled = false;
            }

            highlighted = toggle;
        }

        public void TriggerEquip()
        {
            if (!isInit)
            {
                isInit = true;
                Init();
            }
            player.Equip(this);

            triggerOnToolPickup.Invoke();
        }

        public void TriggerUnequip()
        {
            if (!isInit)
            {
                isInit = true;
                Init();
            }
            player.Unequip(Slot);
            API.updateQuestEvent(typeID, instanceID, questID, false);
        }

        public void DisablePhysics()
        {
            toolPhysics.useGravity = false;
            toolPhysics.isKinematic = true;
        }

        public void EnablePhysics()
        {
            toolPhysics.useGravity = true;
            toolPhysics.isKinematic = false;
        }

        public bool IsInRange(Vector3 a)
        {
            if (Vector3.Magnitude(transform.position - a) < interactRange)
                return true;

            return false;
        }

        public void PickUp(Transform iparent)
        {
            transform.parent = iparent;
            DisablePhysics();
        }

        public void Drop()
        {
            if (transform.parent != null && pickUpAble)
            {
                transform.parent = null;
                EnablePhysics();
            }
        }

        /// <summary>
        /// Generic VR Controller Interaction event with eventType for getting source 
        /// </summary>
        /// <param name="controller"></param>
        public virtual void InteractVR(IMixedRealityController controller, string eventType)
        {
            PlayerHand hand = controller.Visualizer.GameObjectProxy.GetComponent<PlayerHand>();
            Interact(hand, eventType);
        }

        public virtual void Interact(PlayerHand hand, string eventType)
        {
            //If tool is the player hand
            if (this.isHand)
            {
                if (hand.righthand == ((PlayerHand)this).righthand)
                    hand.Activate();
            }
            else
            {
                Debug.Log("Interacting with tool " + typeID + " " + instanceID + " in Quest " + questID + " pickupable " + pickUpAble);

                if (IsInRange(hand.transform.position))
                {
                    if (pickUpAble)
                    {
                        //If a forceps only pickup, check if hand has forceps equipped
                        if (onlyPickUpWithForceps)
                        {
                            if (hand.equipped_tool.Equals(Tools.Forceps))
                                PickUp(hand.transform);
                        }
                        else
                            PickUp(hand.transform);
                    }
                    else
                    {
                        //Try to set the flag
                        API.updateQuestEvent(typeID, instanceID, questID, true);

                        //If flag is set to true, proceed, otherwise not allowed to activate
                        if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
                        {
                            Debug.Log("Equipping " + typeID + " " + instanceID + " in Quest " + questID);
                            Equip(hand);
                            hand.Equip(hand);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generic VR Controller Interaction event with eventType for getting source 
        /// </summary>
        /// <param name="controller"></param>
        public virtual void InteractRelease(IMixedRealityController controller, string eventType)
        {

            PlayerHand hand = controller.Visualizer.GameObjectProxy.GetComponent<PlayerHand>();

            //If tool is the player hand
            if (this.isHand)
            {
                if (hand.righthand == ((PlayerHand)this).righthand)
                    hand.Release();
            }
            else
            {
                Debug.Log("Releasing with tool " + typeID + " " + instanceID + " in Quest " + questID + " pickupable " + pickUpAble);

                Drop();

                if (!pickUpAble)
                    Unequip();
            }
        }

        public virtual void OnClickUp(MixedRealityPointerEventData eventData)
        {
            IMixedRealityController controller = eventData.Pointer.Controller;
            InteractRelease(controller, "OnClickUp");
        }

        //Interaction event with Pointer Handler e.g. buttons and far interaction
        public virtual void OnClick(MixedRealityPointerEventData eventData)
        {
            IMixedRealityController controller = eventData.Pointer.Controller;
            InteractVR(controller, "OnClick");
        }

        //Interaction event with Manipulation e.g. picking up object / near interaction
        public virtual void OnManipulate(Microsoft.MixedReality.Toolkit.UI.ManipulationEventData eventData)
        {
            IMixedRealityController controller = eventData.Pointer.Controller;
            InteractVR(controller, "OnManipulate");
        }

        public virtual void Init()
        {
        }

        public virtual void Equip(PlayerHand hand)
        {
            int B = -1;
            bool isEquippableTool = Tools.TryParse(typeID, out B);

            if (isEquippableTool)
            {
                pickup.Play();
                hand.equipped_tool = B;
                transform.gameObject.SetActive(false);
                isEquipped = true;
            }
        }

        public virtual void Unequip()
        {
            transform.gameObject.SetActive(true);
            isEquipped = false;
        }

        public void RemoveQuest(string iQuestID)
        {
            foreach (Quest quest in quests)
                if (quest.instanceID == iQuestID)
                {
                    quests.Remove(quest);
                    return;
                }
        }
        public void AddQuest(Quest iquest)
        {
            foreach (Quest quest in quests)
                if (quest.instanceID == iquest.instanceID)
                    return;

            quests.Add(iquest);
        }

        public void UpdateQuests(string iFlagID, bool iFlag)
        {
            foreach (Quest quest in quests)
                quest.UpdateFlags(iFlagID, iFlag);
        }

        public virtual void Activate()
        {
        }

        public virtual void Activate(Item tool)
        {
        }

        public virtual void Heat(float temperature)
        {
        }

        public virtual void Release()
        {
        }

        public void Update()
        {
            ToggleHighlight(priority == currentpriority);
        }
    }
}