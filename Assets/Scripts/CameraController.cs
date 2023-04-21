using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField]
    public float radius = 30;

    float selectionDistance;
    Vector3 originalRigidBodyPos, originalScreenTargetPosition;
    private Rigidbody selectedRigidbody;

    void Update()
    {
        if (target == null) return;
        if (Input.GetMouseButton(1))
        {
            RotateAround(target);
        }
        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            var velocity = (originalRigidBodyPos + mousePositionOffset - selectedRigidbody.transform.position) * 50 * Time.deltaTime;
            velocity.y = 0;
            selectedRigidbody.velocity = velocity;
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

    Rigidbody GetRigidbodyFromMouseClick()
    {

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>())
            {
                originalRigidBodyPos = hit.collider.transform.position;
                selectionDistance = Vector3.Distance(ray.origin, hit.point);
                originalScreenTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                return hit.collider.gameObject.GetComponent<Rigidbody>();
            }
            
        }
        return null;
    }
}
