using UnityEngine;

public class TransportableItem : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform holdPoint)
    {
        _rb.useGravity = false;
        _rb.linearDamping = 1f;
        transform.rotation = holdPoint.rotation;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Drop()
    {
        _rb.useGravity = true;
        _rb.linearDamping = 0f;
        _rb.constraints = RigidbodyConstraints.None;
    }
}
