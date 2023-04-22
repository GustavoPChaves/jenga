using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    [SerializeField] float _radius = 20;

    float _selectionDistance;
    Vector3 _originalRigidBodyPosition, _originalScreenTargetPosition;
    Rigidbody _selectedRigidbody;

    void Update()
    {
        MovementCamera();
        InteractWithObjects();
    }

    void FixedUpdate()
    {
        PickObject(_selectedRigidbody);
    }

    /// <summary>
    /// Set the target that camera movements around to
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target)
    {
        this._target = target;
        RotateAround(target);
    }

    /// <summary>
    /// Orbit like camera movement with mouse right button
    /// </summary>
    void MovementCamera()
    {
        if (Input.GetMouseButton(1))
        {
            RotateAround(_target);
        }
    }

    /// <summary>
    /// Select a rigidbody with mouse left click, unselect it after release mouse left button
    /// </summary>
    void InteractWithObjects()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectedRigidbody = GetRigidbodyOnMouseScreenPosition();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _selectedRigidbody = null;
        }
    }

    /// <summary>
    /// Move already selected rigidbody using its velocity
    /// After game tests, the best experience is to zero the velocity y axis, this could be parameterized
    /// </summary>
    /// <param name="selectedRigidbody">A rigidbody to move</param>
    void PickObject(Rigidbody selectedRigidbody)
    {
        if (selectedRigidbody is null) return;
        Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance)) - _originalScreenTargetPosition;
        var velocity = (_originalRigidBodyPosition + mousePositionOffset - selectedRigidbody.transform.position) * 50 * Time.deltaTime;
        velocity.y = 0;
        selectedRigidbody.velocity = velocity;
    }

    /// <summary>
    /// Rotate around a target using mouse position
    /// </summary>
    /// <param name="target"></param>
    void RotateAround(Transform target)
    {
        if (target == null) return;
        var position = target.position + (Vector3.up * 5);
        transform.position = position - (transform.forward * _radius);
        transform.RotateAround(position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(position, transform.right, Input.GetAxis("Mouse Y"));
    }

    /// <summary>
    /// Using Raycast to get the object at mouse position on camera
    /// </summary>
    /// <returns></returns>
    Rigidbody GetRigidbodyOnMouseScreenPosition()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<Rigidbody>())
        {
            _originalRigidBodyPosition = hit.collider.transform.position;
            _selectionDistance = Vector3.Distance(ray.origin, hit.point);
            _originalScreenTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _selectionDistance));
            return hit.collider.gameObject.GetComponent<Rigidbody>();
        }
        return null;
    }
}
