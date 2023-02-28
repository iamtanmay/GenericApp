using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class ReviewWindow : Item
    {
        public float timeTillReset = 5f;
        public bool triggerTimer = false;
        public override int Slot { get { return 1; } }

        public void FixedUpdate()
        {
            if (triggerTimer)
            {
                timeTillReset = timeTillReset - Time.deltaTime;
                if (timeTillReset < 0f)
                {
                    API.ResetApp();
                    //Turn off congratulatory Audio looping
                    triggerOnToolDrop.Invoke();
                    triggerTimer = false;
                    this.enabled = false;
                }
            }
        }

        public override void Activate()
        {
            triggerTimer = true;
        }
    }
}