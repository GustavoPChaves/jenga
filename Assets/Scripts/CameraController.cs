using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    [SerializeField] float radius = 20;

    float selectionDistance;
    Vector3 originalRigidBodyPos, originalScreenTargetPosition;
    Rigidbody selectedRigidbody;

    void Update()
    {
        MovementCamera();
        InteractWithObjects();
    }

    void FixedUpdate()
    {
        PickObject(selectedRigidbody);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        RotateAround(target);
    }

    void MovementCamera()
    {
        if (Input.GetMouseButton(1))
        {
            RotateAround(target);
        }
    }

    void InteractWithObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectedRigidbody = null;
        }
    }

    void PickObject(Rigidbody selectedRigibody)
    {
        if (selectedRigidbody is null) return;
        Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
        var velocity = (originalRigidBodyPos + mousePositionOffset - selectedRigidbody.transform.position) * 50 * Time.deltaTime;
        velocity.y = 0;
        selectedRigidbody.velocity = velocity;
    }

    void RotateAround(Transform target)
    {
        if (target == null) return;
        var position = target.position + (Vector3.up * 5);
        transform.position = position - (transform.forward * radius);
        transform.RotateAround(position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(position, transform.right, Input.GetAxis("Mouse Y"));
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<Rigidbody>())
        {
            originalRigidBodyPos = hit.collider.transform.position;
            selectionDistance = Vector3.Distance(ray.origin, hit.point);
            originalScreenTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
            return hit.collider.gameObject.GetComponent<Rigidbody>();
        }
        return null;
    }
}
