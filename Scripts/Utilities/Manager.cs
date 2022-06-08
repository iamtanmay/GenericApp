using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Generic Manager can be used to build flexible pipelines
/// </summary>
public class Manager : MonoBehaviour
{
    public List<UnityEvent> InitEvents; 
    public List<UnityEvent> UpdateEvents;

    public void Start(){
        foreach(UnityEvent func in InitEvents)
            func.Invoke();
    }

    public void FixedUpdate(){
        foreach(UnityEvent func in UpdateEvents)
            func.Invoke();
    }
}