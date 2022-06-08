using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class DummyTool : Tool
    {
        public override int Slot { get { return -1; } }

        public override void Init()
        {
        }

        public override void Equip()
        {
        }

        public override void Unequip()
        {
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