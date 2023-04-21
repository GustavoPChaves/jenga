using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;
    public float radius = 10;
    void Update()
    {
        if (target == null) return;
        var position = target.position + (Vector3.up * 10);
        transform.position = position - (transform.forward * radius);
        transform.RotateAround(position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(position, transform.right, Input.GetAxis("Mouse Y"));
    }
}
