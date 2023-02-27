using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    public string ID;

    void Awake()
    {
        ID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        PersistentObject[] listObjects = GameObject.FindObjectsOfType<PersistentObject>();

        for (int i=0; i<listObjects.Length; i++)
        {
            if (listObjects[i] != this)
                if (listObjects[i].ID == ID)
                    Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
