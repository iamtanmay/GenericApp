using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class MagnesiumRoll : Tool
    {
        public Transform MagnesiumStrip, newStrip, oldStrip, TemplateStrip;
        public Transform stripCreationPoint;
        public Animation anim;

        public override int Slot { get { return -1; } }

        public override void Init()
        {
            CreateNewStrip(); 
        }

        public override void Equip()
        {
        }

        public override void Unequip()
        {
        }

        public void CreateNewStrip()
        {
            if (newStrip == null)
            {
                newStrip = Transform.Instantiate(MagnesiumStrip);
                newStrip.position = stripCreationPoint.position;
                newStrip.rotation = Quaternion.Euler(new Vector3(90f, 0f, 90f));
                newStrip.gameObject.SetActive(false);
                newStrip.GetComponent<MagnesiumStrip>().triggerOnToolPickup = TemplateStrip.GetComponent<MagnesiumStrip>().triggerOnToolPickup;
                newStrip.GetComponent<MagnesiumStrip>().triggerOnToolDrop = TemplateStrip.GetComponent<MagnesiumStrip>().triggerOnToolDrop;
            }
        }

        public void OnClick()
        {
            triggerOnToolPickup.Invoke();
            if (oldStrip != null)
            {
                Debug.Log("Inside Existing Strip Creation");
                oldStrip.GetComponent<Rigidbody>().useGravity = true;
                oldStrip.GetComponent<Animation>().Play("Strip_Flutter");
                oldStrip.GetComponent<MagnesiumStrip>()._selfDestruct = 600;
            }

            if (newStrip != null)
            {
                anim.Play("Spin");
                newStrip.gameObject.SetActive(true);
                newStrip.GetComponent<MagnesiumStrip>().Create();
                oldStrip = newStrip;
                newStrip = null;
                CreateNewStrip();
            }
        }

        public override void OnClick(MixedRealityPointerEventData eventData)
        {
        }

        public override void TriggerTool()
        {
        }

        public override void TriggerRelease()
        {
        }
    }
}
