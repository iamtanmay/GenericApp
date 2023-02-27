using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class DummyTool : Tool
    {
        public override int Slot { get { return -1; } }
    }
}