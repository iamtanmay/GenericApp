using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_HeightAdjust : MonoBehaviour
{
    public float offset;
    public Transform[] objects;
    public AppSettings.Config config;

    public void InitializeHeight()
    {
        Debug.Log("Initialise height " + config.height);
        if (config.height<0)
            for (int i = config.height; i < 0; i++)
                foreach (Transform obj in objects)
                    obj.position = new Vector3(obj.position.x, obj.position.y - offset, obj.position.z);
        else
            for (int i = 0; i < config.height; i++)
                foreach (Transform obj in objects)
                    obj.position = new Vector3(obj.position.x, obj.position.y + offset, obj.position.z);
    }

    public void centerWorld(Vector3 ioffset)
    {
        foreach (Transform obj in objects)
            obj.position = obj.position + ioffset;
    }

    public void Up()
    {
        foreach (Transform obj in objects)
            obj.position = new Vector3(obj.position.x, obj.position.y + offset, obj.position.z);

        config.height++;
        config.SaveToFile();
    }

    public void Down()
    {
        foreach (Transform obj in objects)
            obj.position = new Vector3(obj.position.x, obj.position.y - offset, obj.position.z);

        config.height--;
        config.SaveToFile();
    }
}
