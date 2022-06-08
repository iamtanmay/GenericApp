using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtByAxis : MonoBehaviour
{
    public Transform target;
    public float RotationSpeed;
    public bool x = false, y=false, z=false;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        _direction = (target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        Vector3 eulerAngles = transform.rotation.eulerAngles;

        if (x)
            eulerAngles.x = newRotation.eulerAngles.x;

        if (y)
            eulerAngles.y = newRotation.eulerAngles.y;

        if (z)
            eulerAngles.z = newRotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}