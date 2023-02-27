using System;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Generic Manager can be used to build flexible pipelines
/// </summary>
public class Manager : MonoBehaviour
{
    public List<UnityEvent> initMethods; 
    public List<UnityEvent> updateMethods;
    
    public List<KeyValuePair<UnityEvent<string>>> events = new List<KeyValuePair<UnityEvent<string>>>();
    Dictionary<string, UnityEvent<string>> _events = new Dictionary<string, UnityEvent<string>>();

    public void SyncEventList()
    {
        foreach (var kvp in events)
            _events[kvp.key] = kvp.val;
    }

    public void Start(){
        SyncEventList();
        foreach (UnityEvent func in initMethods)
            func.Invoke();
    }

    public void FixedUpdate(){
        foreach(UnityEvent func in updateMethods)
            func.Invoke();
    }

    public void CallEvent(string iEventName)    
    {
        string temp = iEventName.Replace(")", "");
        string[] components = temp.Split("(");
        if (_events.ContainsKey(components[0])){
            if (components.Length > 1)
                _events[components[0]].Invoke(components[1]);
            else
                _events[components[0]].Invoke("");
        }
    }
}