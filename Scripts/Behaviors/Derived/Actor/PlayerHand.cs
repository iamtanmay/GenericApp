using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class PlayerHand : Tool
    {
        public Material glovemat, handmat2, handmat, invisiblemat;

        //Equippable tools
        public Transform[] equippableTools;

        public Tool equippedTool;

        public bool isSimulatedHand = false;

        public Animation animator;

        public AnimationClip point, pinch, release;

        public SkinnedMeshRenderer handRenderer;

        public bool isgloved = false;

        public bool isHandEnabled = false;

        public int equipped_tool = -1;

        public bool righthand = false;

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

        public void InitAnimation()
        {
            //Check which hand this is
            if (transform.name.Contains("HandRight"))
                righthand = true;

            if (righthand)
            {
                point = animator.GetClip("RightHandPoint");
                pinch = animator.GetClip("RightHandPinch");
                release = animator.GetClip("RightHandRelease");

            }
            else
            {
                point = animator.GetClip("LeftHandPoint");
                pinch = animator.GetClip("LeftHandPinch");
                release = animator.GetClip("LeftHandRelease");
            }

            animator.clip = point;
            animator.Play();
        }

        public override void Init()
        {
            if (isHandEnabled)
            {
                //Check which hand this is
                if (transform.name.Contains("HandRight"))
                    righthand = true;

                //Get current hand equipment from Player
                isgloved = player.Slots[1];

                string handPrefix = "L";

                if (righthand)
                    handPrefix = "R";

                handRenderer = transform.Find(handPrefix + "_Hand_MRTK_Rig/R_Hand").GetComponent<SkinnedMeshRenderer>();

                for (int i=1; i<=equippableTools.Length; i++)
                    equippableTools[i] = transform.Find(handPrefix + "_Hand_MRTK_Rig/R_Wrist/" + ((ToolType)i));

                if (!isSimulatedHand)
                    InitAnimation();

                Equip(this);
            }
        }

        public override void Equip(PlayerHand hand)
        {

            Material[] materials;

            //Gloves
            if (isgloved)
            {
                //If hand is null then this is a secondary equip and
                //we were called to equip by the other hand already
                if (hand != null)
                {
                    if (righthand)
                    {
                        if (player.leftHand == null)
                            player.initHands();

                        if (player.leftHand != null)
                        {
                            player.leftHand.isgloved = true;
                            player.leftHand.Equip(null);
                        }
                    }
                    else
                    {
                        if (player.rightHand == null)
                            player.initHands();

                        if (player.rightHand != null)
                        {
                            player.rightHand.isgloved = true;
                            player.rightHand.Equip(null);
                        }
                    }
                }

                materials = handRenderer.materials;
                materials[0] = glovemat;
                materials[1] = invisiblemat;
                handRenderer.materials = materials;
                player.Slots[1] = true;
            }

            //Common tasks for all tools
            if (equipped_tool >= 0)
            {
                materials = handRenderer.materials;
                materials[0] = invisiblemat;
                materials[1] = invisiblemat;
                handRenderer.materials = materials;

                if (righthand)
                    player.EquippedTools[3] = equippedTool;
                else
                    player.EquippedTools[2] = equippedTool;

                if (equipped_tool > 0)
                    ActivateTool(equippableTools[equipped_tool]);
            }
        }

        public void ActivateTool(Transform iobject)
        {
            iobject.gameObject.SetActive(true);
            equippedTool = iobject.GetComponent<Tool>();
            equippedTool.API = API;
        }

        public override void Unequip()
        {
            Material[] materials;

            //Gloves
            if (!isgloved)
            {
                materials = handRenderer.materials;
                materials[0] = handmat;
                materials[1] = handmat2;
                handRenderer.materials = materials;

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

        public override void Activate()
        {
            if (equippedTool != null)
            {
                equippedTool.Activate();
            }
            else if (!isSimulatedHand)
            {
                this.animator.clip = pinch;
                animator.Play();
            }
        }

        public override void Release()
        {
            if (equippedTool != null)
            {
                equippedTool.Release();
            }
            else if (!isSimulatedHand)
            {
                animator.clip = release;
                animator.Play();
            }
        }
    }
}