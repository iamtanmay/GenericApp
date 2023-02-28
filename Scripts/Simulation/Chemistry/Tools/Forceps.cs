using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class Forceps : Item
    {
        public bool open = false;

        public override int Slot { get { return 1; } }

        public override void Init()
        {
            open = false;
            anim.Play("forceps_open");
        }

        public override void Activate()
        {
            anim.Play("forceps_close");
        }

        public override void Activate(Item tool)
        {
        }

        public override void Release()
        {
            anim.Play("forceps_open");
        }
    }
}