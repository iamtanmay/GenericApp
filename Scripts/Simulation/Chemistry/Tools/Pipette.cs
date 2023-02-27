using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class Pipette : Tool
    {
        public Transform Prefab_liquidDrop;

        public Transform dropCreationPoint;

        public bool open = false;

        public override int Slot { get { return 1; } }

        public override void Init()
        {
        }

        public override void Activate()
        {
            if (!isInit)
                Init();

            anim.Play("forceps_close");

            //Try to set the flag
            API.updateQuestEvent(typeID, instanceID, questID, true);

            //If flag is set to true, proceed, otherwise not allowed to activate
            if (API.getQuestFlag(typeID, "Flags", "EventNames", questID) == 1)
            {
                triggerOnToolPickup.Invoke();
                CreateNewDrop();
            }
        }

        public override void Activate(Tool tool)
        {
            //If its a container
            ChemicalContainer chemicalContainer;
            bool exists = tool.transform.TryGetComponent<ChemicalContainer>(out chemicalContainer);

            if (exists)
                contents = chemicalContainer.contents;
        }

        public override void Release()
        {
            anim.Play("forceps_open");
        }

        public void CreateNewDrop()
        {
            Transform liquidDrop = Transform.Instantiate(Prefab_liquidDrop);
            liquidDrop.parent = dropCreationPoint;
            liquidDrop.localPosition = new Vector3(0, 0, 0);
            liquidDrop.position = dropCreationPoint.position;
            liquidDrop.rotation = Quaternion.Euler(new Vector3(90f, 0f, 90f));
            liquidDrop.parent = null;
        }
    }
}