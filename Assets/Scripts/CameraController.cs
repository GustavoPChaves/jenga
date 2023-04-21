using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField]
    public float radius = 30;
    void Update()
    {
        if (target == null) return;
        if (Input.GetMouseButton(1))
        {
            RotateAround(target);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        RotateAround(target);
    }

    void RotateAround(Transform target)
    {
        var position = target.position + (Vector3.up * 5);
        transform.position = position - (transform.forward * radius);
        transform.RotateAround(position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(position, transform.right, Input.GetAxis("Mouse Y"));
    }
}
