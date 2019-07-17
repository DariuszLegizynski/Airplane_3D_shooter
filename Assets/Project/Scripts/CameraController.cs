using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float zOffset;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(
                target.transform.position.x,
                0f,
                target.transform.position.z - zOffset
                );
        }
    }
}
