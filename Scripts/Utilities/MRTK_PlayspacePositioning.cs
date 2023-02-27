using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;

public class MRTK_PlayspacePositioning : MonoBehaviour
{
    public Quaternion targetRot;
    public Vector3 offsetSize;
    public float tooFar;
    public Transform aimTransform;
    public VR_HeightAdjust offsetTool;
    public Camera main;

    [ContextMenu("Reposition")]
    public void Reposition()
    {
        Vector3 offset = main.transform.position - aimTransform.position;
        offsetTool.centerWorld(offset);
        //MixedRealityPlayspace.Transform.position = aimTransform.position;
        MixedRealityPlayspace.Rotation = targetRot;
    }

    [ContextMenu("Center")]
    public void Center()
    {
        MixedRealityPlayspace.Rotation = targetRot;
        main = Camera.main;
        Vector3 angleA = targetRot.eulerAngles;
        Vector3 angleB = main.transform.localRotation.eulerAngles;
        angleA.y =  2*angleA.y - angleB.y;
        MixedRealityPlayspace.Rotation = Quaternion.Euler(angleA);        
    }

    [ContextMenu("ApplyOffset")]
    public void OffsetPosition(int iAmount)
    {
        MixedRealityPlayspace.Position = MixedRealityPlayspace.Position + iAmount * offsetSize;
    }

    public void Update()
    {
        if (Vector3.Magnitude(main.transform.position - aimTransform.position) > tooFar)
            Reposition();
    }
}