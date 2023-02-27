using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppStarter
{
    public class JournalQuestUI : MonoBehaviour
    {
        public List<JournalObjectiveUI> objectiveUIs = new List<JournalObjectiveUI>();

        public void SetText(int index, string iText)
        {
            objectiveUIs[index].description.text = iText;
        }

        public void SetHighlightFX(int index)
        {
            //Pulse the outline
            objectiveUIs[index].fx.enabled = true;
            objectiveUIs[index].fx.TriggerFX(1, 0);
        }

        public void SetSuccessFX(int index)
        {
            Debug.Log("Flag success " + index);
            objectiveUIs[index].fx.DisableAllFX();
            objectiveUIs[index].fx.enabled = true;
            //Play success Audio
            objectiveUIs[index].fx.PlayAudio(0);
            //Grey it out
            objectiveUIs[index].fx.TriggerFX(3, 0);
        }
        public void RemoveSuccessFX(int index)
        {
            Debug.Log("Flag success " + index);
            objectiveUIs[index].fx.DisableAllFX();
            objectiveUIs[index].fx.enabled = true;
        }

        public void SetFailureFX(int index)
        {
            Debug.Log("Flag success " + index);
            objectiveUIs[index].fx.DisableAllFX();
            //Play failure Audio
            objectiveUIs[index].fx.PlayAudio(1);
            objectiveUIs[index].fx.enabled = false;
        }
    }
}