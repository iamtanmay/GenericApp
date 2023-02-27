using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APICache : MonoBehaviour
{
    public API API;

    //Search for the API component
    public void Initialise()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("API");

        if (temp != null)
            API = temp.GetComponent<API>();
    }

    public void Awake()
    {
        Initialise();
    }
}
