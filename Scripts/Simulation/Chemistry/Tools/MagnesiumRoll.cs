using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class MagnesiumRoll : Item
    {
        public Transform Prefab_MagnesiumStrip;
        Transform newStrip, oldStrip;
        public Transform stripCreationPoint;
        public Animation anim;

        public override int Slot { get { return -1; } }

        public override void Init()
        {
            CreateNewStrip(); 
        }

        public void CreateNewStrip()
        {
            if (newStrip == null)
            {
                newStrip = Transform.Instantiate(Prefab_MagnesiumStrip);
                newStrip.parent = stripCreationPoint;
                newStrip.localPosition = new Vector3(0, 0, 0);
                newStrip.position = stripCreationPoint.position;
                newStrip.rotation = Quaternion.Euler(new Vector3(90f, 0f, 90f));
                newStrip.gameObject.SetActive(false);
            }
        }

        public override void Activate()
        {
            if (!isInit)
                Init();

            //Try to set the flag
            API.updateQuestEvent(typeID, instanceID, questID, true);

            //If flag is set to true, proceed, otherwise not allowed to activate
            if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
            {
                triggerOnToolPickup.Invoke();
                if (oldStrip != null)
                {
                    oldStrip.GetComponent<MagnesiumStrip>().EnablePhysics();
                    oldStrip.GetComponent<Animation>().Play("Strip_Flutter");
                    oldStrip.GetComponent<MagnesiumStrip>()._selfDestruct = 200;
                }

                if (newStrip != null)
                {
                    anim.Play("Spin");
                    newStrip.gameObject.SetActive(true);
                    newStrip.parent = null;
                    newStrip.GetComponent<MagnesiumStrip>().API = API;
                    newStrip.GetComponent<MagnesiumStrip>().questID = questID;
                    newStrip.GetComponent<MagnesiumStrip>().Create();
                    oldStrip = newStrip;
                    newStrip = null;
                    CreateNewStrip();
                }
            }
        }
    }
}
