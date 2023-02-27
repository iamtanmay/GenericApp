using System;
using System.Reflection;
using System.Collections.Generic;
using Popcron.Console;
using Utilities;
using ObjectMaps;
using AppStarter;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;


public class API : MonoBehaviour
{
    //Variables
    public int RoomTemperature = 300;

    public List<KeyValuePair<TransformCollection>> containers = new List<KeyValuePair<TransformCollection>>();

    public TransformMap instances;

    public PrefabMap prefabs;

    public Transform Containers, Review;

    public SceneTransition sceneTransition;

    public API_Extensions API_Extensions;

    public Dictionary<string, MethodInfo> APIMethods;

    //Tools cache
    public Tool[] sceneTools;

    //Quests
    public List<Transform> quests = new List<Transform>();

    public List<string> questIDs = new List<string>();

    public List<Variables> questVarsList = new List<Variables>();

    public List<List<bool>> questVarFlagsList = new List<List<bool>>();

    public List<List<string>> questVarFlagNamesList = new List<List<string>>();

    public List<bool> flags = new List<bool>();

    public List<string> flagNames = new List<string>();

    public void Init()
    {
        Create_API_Dictionary();
    }

    //Store all API functions in dictionary
    public void Create_API_Dictionary()
    {
        APIMethods = new Dictionary<string, System.Reflection.MethodInfo>();

        foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
            if (!APIMethods.ContainsKey(method.Name))
                APIMethods.Add(method.Name, method);

        foreach (MethodInfo method in API_Extensions.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
            if (!APIMethods.ContainsKey(method.Name))
                APIMethods.Add(method.Name, method);

        foreach (MethodInfo method in typeof(MonoBehaviour).GetMethods(BindingFlags.Public | BindingFlags.Instance))
            APIMethods.Remove(method.Name);

        //Remove this function
        APIMethods.Remove("CreateDictionaryOfAPIMethods");
    }

    public void AssignDialogue(string npcID, string dialogueID)
    {

    }

    public void StartDialogue(string npcID)
    {

    }

    public void StartReview()
    {
        Debug.Log("Review started");
        Review.gameObject.SetActive(true);
    }

    public void CacheAllTools()
    {
        sceneTools = Containers.GetComponentsInChildren<Tool>();
    }

    public void ResetApp()
    {
        sceneTransition.Activate();
    }

    //What event the quest is at currently for highlighting next event
    public void updateCurrentPriority(int currPriority)
    {
        Debug.Log("Updating current Event to " + currPriority);
        foreach (Tool tool in sceneTools)
            tool.currentpriority = currPriority;
    }

    /// <summary>
    /// Returns 0 if Quest flag is false, 1 if true, -1 if error
    /// </summary>
    /// <param name="flagID">Name of flag to check</param>
    /// <param name="flagsList">Name of bool flags List</param>
    /// <param name="flagNamesList">Name of flag names List</param>
    /// <param name="questID">ID of Quest</param>
    /// <returns></returns>wdd
    public int getQuestFlag(string flagID, string flagsList, string flagNamesList, string questID)
    {
        Debug.Log("Is flag set ? " + flagID + " in " + flagsList + ", " + flagNamesList + " of quest " + questID);
        int index = questIDs.FindIndex(a => a == questID);

        if (index != -1)
        {
            Transform newItem = GetInstance("" + index);
            Variables questVar = newItem.gameObject.GetComponent<Variables>();
            flags = questVar.declarations.Get<List<bool>>("Flags");
            flagNames = questVar.declarations.Get<List<string>>("EventNames");

            int index2 = flagNames.FindIndex(a => a == flagID);

            if (index2 != -1 && index2 < flags.Count)
            {
                if (flags[index2])
                {
                    Debug.Log("Yes");
                    return 1;
                }
                Debug.Log("No");
                return 0;
            }
        }
        Debug.Log("Not found");
        return -1;
    }

    public void updateQuestEvent(string typeID, string instanceID, string questID, bool flagVal)
    {
        Debug.Log("Setting updateFlag trigger " + typeID + " " + instanceID + " in Quest " + questID + " set to " + flagVal);

        int index = questIDs.FindIndex(a => a == questID);

        if (index != -1)
            CustomEvent.Trigger(quests[index].gameObject, "objectiveChanged", typeID, flagVal, instanceID);
    }

    [Command("EndQuest")]
    public void DeleteQuest(string ID)
    {
        Transform quest = instances.GetItem(ID);

        if (quest == null) return;

        int index = questIDs.FindIndex(a => a == ID);

        quests.RemoveAt(index);
        questIDs.RemoveAt(index);
        instances.RemoveItem(ID);
        GameObject.DestroyImmediate(quest);
    }

    [Command("DespawnList")]
    public void Despawnlist(List<string> IDs)
    {
        for (int i = 0; i < IDs.Count; i++)
            Despawn(IDs[i]);
    }

    [Command("Despawn")]
    public void Despawn(string ID)
    {
        Debug.Log("Despawning " + ID);
        Transform t = instances.GetItem(ID);

        if (t == null) return;

        instances.RemoveItem(ID);
        GameObject.DestroyImmediate(t.gameObject);
        Debug.Log("Despawned " + ID);
    }

    [Command("StartQuest")]
    public void StartQuest(string ID)
    {
        SpawnQuest(ID);
    }

    [Command("SpawnQuest")]
    public string SpawnQuest(string ID)
    {
        Debug.Log("Triggered " + ID);

        Transform prefab = prefabs.GetItem("Quests", ID);
        if (prefab == null) return "";

        Transform newItem = GameObject.Instantiate(prefab);
        string newID = instances.AddItem("Quests", newItem);
        questIDs.Add(newID);
        newItem.position = new Vector3();
        newItem.gameObject.name = ID + "_" + newID;
        quests.Add(newItem);

        Variables questVar = newItem.gameObject.GetComponent<Variables>();

        questVarsList.Add(questVar);
        questVarFlagsList.Add(questVar.declarations.Get<List<bool>>("Flags"));
        questVarFlagNamesList.Add(questVar.declarations.Get<List<string>>("EventNames"));

        CustomEvent.Trigger(newItem.gameObject, "StartQuest", newID);
        return questIDs[questIDs.Count - 1];
    }

    public void AttachAPI(Transform iItem, string iID, string questID)
    {
        if (iItem == null) return;

        Tool tool = iItem.GetComponent<Tool>();

        //Search through children
        if (tool == null)
        {
            foreach (Transform child in iItem)
            {
                tool = child.GetComponent<Tool>();
                if (tool != null) break;
            }
        }

        if (tool == null) return;

        tool.API = this;
        tool.questID = questID;
        tool.instanceID = iID;
    }

    [Command("SpawnAtPos")]
    public string SpawnAtPos(string iCategory, string iPrefab, Vector3 ipos, string questID)
    {
        Transform prefab = prefabs.GetItem(iCategory, iPrefab);

        if (prefab == null) return "";

        Transform newItem = GameObject.Instantiate(prefab);
        newItem.position = ipos;
        string oID = instances.AddItem(iCategory, newItem);
        AttachAPI(newItem, oID, questID);
        return oID;

    }

    [Command("SpawnList")]
    public List<string> SpawnList(List<List<string>> spawnlist, string questID)
    {
        List<string> returnval = new List<string>();

        foreach (List<string> spawn in spawnlist)
            if (spawn.Count > 3)
                returnval.Add(SpawnInContainer(spawn[0], spawn[1], spawn[2], spawn[3], questID));

        return returnval;
    }

    [Command("SpawnInContainer")]
    public string SpawnInContainer(string iCategory, string iPrefab, string iContainer, string iSlot, string questID)
    {
        Transform container = null;

        foreach (KeyValuePair<TransformCollection> item in containers)
            if (item.key == iContainer)
            {
                container = item.val[iSlot];
            }

        Transform prefab = prefabs.GetItem(iCategory, iPrefab);

        Debug.Log("ToSpawn " + iPrefab);

        if (prefab == null) return "";

        Debug.Log("InContainer " + iContainer);

        if (container == null) return "";

        Debug.Log("Spawned " + iPrefab);

        Transform newItem = GameObject.Instantiate(prefab);
        newItem.parent = container;
        newItem.localPosition = new Vector3();
        newItem.localRotation = Quaternion.identity;
        newItem.gameObject.SetActive(true);
        string oID = instances.AddItem(iCategory, newItem);
        AttachAPI(newItem, oID, questID);
        return oID;
    }

    public void PlaceInContainer(Transform iobject, string iContainer, string iSlot)
    {
        Transform container = null;

        foreach (KeyValuePair<TransformCollection> item in containers)
            if (item.key == iContainer)
            {
                container = item.val[iSlot];
                iobject.parent = container;
                iobject.localPosition = new Vector3();
                iobject.localRotation = Quaternion.identity;
            }
    }

    public void PlaceInContainer(string ID, string iContainer, string iSlot)
    {
        Transform iobject = GetInstance(ID);

        if (iobject)
            PlaceInContainer(iobject, iContainer, iSlot);
    }

    [Command("SpawnInContainerReturnTransform")]
    public Transform SpawnTransform(string iCategory, string iPrefab, string iContainer, string iSlot, string questID)
    {
        Transform container = null;

        foreach (KeyValuePair<TransformCollection> item in containers)
            if (item.key == iContainer)
                container = item.val[iSlot];

        Transform prefab = prefabs.GetItem(iCategory, iPrefab);


        if (prefab == null) return null;
        if (container == null) return null;

        Transform newItem = GameObject.Instantiate(prefab);
        newItem.parent = container;
        newItem.localPosition = new Vector3();
        newItem.localRotation = Quaternion.identity;
        newItem.gameObject.SetActive(true);        
        string oID = instances.AddItem(iCategory, newItem);
        AttachAPI(newItem, oID, questID);
        return newItem;
    }

    public Transform GetInstance(string categoryID, string instanceID)
    {
        return instances.GetItem(categoryID, instanceID);
    }

    public Transform GetInstance(string ID)
    {
        return instances.GetItem(ID);
    }

    public NPC GetNPC(string ID)
    {
        return instances.GetItem(ID).GetComponent<NPC>();
    }

    [Command("GetNPCs")]
    public string GetNPCsInScene()
    {
        string return_val = "!";


        return return_val;
    }

    //when the object gets created, register it
    private void OnEnable()
    {
        Parser.Register(this, "App");
    }

    //when the object gets destroyed, unregister it
    private void OnDisable()
    {
        Parser.Unregister(this);
    }
}