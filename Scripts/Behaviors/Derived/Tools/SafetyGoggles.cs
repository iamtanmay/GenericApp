using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
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

        public override void Equip()
        {
            CameraOverlay.SetActive(true);
            transform.position = InitialPos;
            transform.rotation = Quaternion.Euler(InitialRot);
            transform.gameObject.SetActive(false);
            isEquipped = true;
            triggerOnToolPickup.Invoke();
        }

        public override void Unequip()
        {
            CameraOverlay.SetActive(false);
            transform.gameObject.SetActive(true);
            isEquipped = false;
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