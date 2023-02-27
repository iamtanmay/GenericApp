using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace AppStarter
{
    public class JournalObjectiveUI : MonoBehaviour
    {
        public int ID = -1;

        public bool achieved = false;

        public TMPro.TextMeshPro description;
        public FXSystem fx;

        public void SetText(string iText)
        {
            description.text = iText;
        }

        public void SetHighlightFX()
        {
            //Pulse the outline
            fx.enabled = true;
            fx.TriggerFX(1, 0);
        }

        public void SetSuccessFX()
        {
            fx.DisableAllFX();
            fx.enabled = true;
            //Play success Audio
            fx.PlayAudio(1);
            //Grey it out
            fx.TriggerFX(3,0);
        }
        public void RemoveSuccessFX()
        {
            fx.DisableAllFX();
            fx.enabled = true;
        }

        public void SetFailureFX()
        {
            fx.enabled = true;
            //Play failure Audio
            fx.PlayAudio(2);
        }
    }
}