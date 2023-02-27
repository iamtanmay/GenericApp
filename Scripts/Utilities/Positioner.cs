using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    public Vector3 targetPos;
    public Quaternion targetRot;
    public Vector3 offsetSize;

    [ContextMenu("Reposition")]
    public void Reposition()
    {
        transform.localPosition = targetPos;
        transform.localRotation = targetRot;
    }

    [ContextMenu("SetToZero")]
    public void SetToZero()
    {
        transform.localPosition = new Vector3();
        transform.localRotation = Quaternion.identity;
    }

    [ContextMenu("ApplyOffset")]
    public void OffsetPosition(int iAmount)
    {
        transform.localPosition = transform.localPosition + iAmount * offsetSize;
    }
}
