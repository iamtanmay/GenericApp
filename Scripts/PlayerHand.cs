using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace VRApplication
{
    public class PlayerHand : Tool
    {
        public Material glovemat, handmat, invisiblemat;

        public Transform forceps;

        public Tool equippedTool;

        public SkinnedMeshRenderer handRenderer;

        public bool isgloved = false;

        //0 normal, 1 forceps
        public int equipped_tool = 0;

        bool righthand = false;

        public override int Slot
        {
            get
            {
                if (righthand)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }

        public override void Init()
        {
            //Check which hand this is
            if (transform.name.Contains("HandRight"))
                righthand = true;

            //Get current hand equipment from Player
            isgloved = player.Slots[1];

            if (righthand && (player.EquippedTools[3].GetType() == typeof(Forceps)))
                equipped_tool = 1;

            if (!righthand && (player.EquippedTools[2].GetType() == typeof(Forceps)))
                equipped_tool = 1;

            if (righthand)
            {
                handRenderer = transform.Find("R_Hand_MRTK_Rig/R_Hand").GetComponent<SkinnedMeshRenderer>();
                forceps = transform.Find("R_Hand_MRTK_Rig/R_Wrist/Forceps_Player");
            }
            else
            {
                handRenderer = transform.Find("L_Hand_MRTK_Rig/L_Hand").GetComponent<SkinnedMeshRenderer>();
                forceps = transform.Find("L_Hand_MRTK_Rig/L_Wrist/Forceps_Player");
            }

            Equip();
        }

        public override void Equip()
        {
            //Gloves
            if (isgloved)
            {
                handRenderer.material = glovemat;
                player.Slots[1] = true;
            }

            //Forceps
            if (equipped_tool == 1)
            {
                forceps.gameObject.SetActive(true);
                equippedTool = forceps.GetComponent<Tool>();
            }

            //Common tasks for all tools
            if (equipped_tool > 0)
            {
                handRenderer.material = invisiblemat;

                if (righthand)
                    player.EquippedTools[3] = equippedTool;
                else
                    player.EquippedTools[2] = equippedTool;
            }
        }

        public override void Unequip()
        {
            handRenderer.material = glovemat;

            //Gloves
            if (!isgloved)
            {
                handRenderer.material = handmat;
                player.Slots[1] = false;
            }

            if (righthand)
                player.Unequip(3);
            else
                player.Unequip(2);

            if(equippedTool != null)
            {
                equippedTool.transform.gameObject.SetActive(false);
                equippedTool = null;
            }
        }

        public override void OnClick(MixedRealityPointerEventData eventData)
        {
        }

        public override void TriggerTool()
        {
            if (equippedTool != null)
            {
                equippedTool.TriggerTool();
            }
        }

        public override void TriggerRelease()
        {
            if (equippedTool != null)
            {
                equippedTool.TriggerRelease();
            }
        }
    }
}