using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRaycastObject : MonoBehaviour
{
    public float range;
    public bool returnOnlyFirst;
    public LayerMask layerMask;
    public string objectTag;
    public Transform origin;
    public LineRenderer lineRender;

    public void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        CreateRay();
    }

    public void CreateRay()
    {
        lineRender.SetPosition(0, origin.position);
        lineRender.SetPosition(1, origin.forward * range);
        //Debug.DrawRay(origin.position, origin.forward, Color.green);

        List<GameObject> objects = Execute();

        foreach(GameObject obj in objects)
            Debug.Log(obj.name);
    }

    /// <summary>
    /// Returns list of gameobjects the ray collides with
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<GameObject> Execute()
    {
        List<GameObject> result = new List<GameObject>();
        // Reset ray with new mouse position
        Ray ray = new Ray(origin.position, origin.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, range, layerMask);

        foreach (RaycastHit hit in hits)
        {
            GameObject tobject = hit.collider.gameObject;
            if (objectTag.Length > 0)
            {
                if (tobject.tag == objectTag)
                    result.Add(tobject);
            }
            else
                result.Add(tobject);

            if (result.Count > 0 && returnOnlyFirst)
                return result;
        }
        return result;
    }
}