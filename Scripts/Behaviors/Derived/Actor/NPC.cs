using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AppStarter
{
    //NPC class
    public class NPC : Item
    {
        public Transform dialogueMount;

        public Dictionary<string, Transform> dialogues = new Dictionary<string, Transform>();

        public string dialogue_folder;

        public bool speaking = false;

        public string dialogueOnAwake = "";

        public string fullpath = "";

        public float dialogueDuration;

        public AudioSource _audiosource;

        public override int Slot { get { return -1; } }

        public override void Init()
        {
            //Load dialogue prefabs
            fullpath = dialogue_folder + "/" + typeID;
            Transform[] dialoguePrefabs = Resources.LoadAll<Transform>(dialogue_folder + "/" + typeID) as Transform[];

            foreach (Transform prefab in dialoguePrefabs)
            {
                Debug.Log(prefab.name + " added");
                Transform instantiated = GameObject.Instantiate(prefab) as Transform;
                dialogues.Add(prefab.name, instantiated);
            }

            foreach (KeyValuePair<string, Transform> idialogue in dialogues)
            {
                idialogue.Value.parent = dialogueMount;
                idialogue.Value.localPosition = new Vector3();
                idialogue.Value.localRotation = new Quaternion();
                idialogue.Value.localScale = new Vector3(1f, 1f, 1f);                
                idialogue.Value.gameObject.SetActive(false);
            }

            if (dialogueOnAwake != "")
                Speak(dialogueOnAwake);
        }

        public void FinishedSpeaking()
        {
            speaking = false;
        }

        public bool Speak(string ID)
        {
            if (speaking)
                return false;

            dialogues[ID].gameObject.SetActive(true);
            InitializeDialogue(dialogues[ID].gameObject);
            speaking = true;
            return true;
        }

        public void InitializeDialogue(GameObject idialogueObject)
        {
            _audiosource = idialogueObject.GetComponent<AudioSource>();
            _audiosource.Play();
            dialogueDuration = _audiosource.clip.length;
            StartCoroutine(WaitForAudio(idialogueObject));
        }

        IEnumerator WaitForAudio(GameObject idialogueObject)
        {
            yield return new WaitForSeconds(dialogueDuration);

            idialogueObject.gameObject.SetActive(false);
            speaking = false;
        }

        public override void Activate()
        {
        }

        public override void Activate(Item tool)
        {
        }

        public override void Release()
        {
        }
    }
}