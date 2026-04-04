using UnityEngine;

public class HoldAbility : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;

    [SerializeField] private float _maxThrowingStrength = 2f;
    [SerializeField] private float _holdStrength = 100f;
    [SerializeField] private float _damping = 100f;
    [SerializeField] private float _maxForce = 200f;
    [SerializeField] private float _maxDistance = float.MaxValue;

    public GameObject HeldObject { get; private set; }
    public Transform HoldPoint => _holdPoint;

    private Rigidbody _transportableRb;

    private void FixedUpdate()
    {
        HandleHolding();
    }

    public void TryPickUp(GameObject obj)
    {
        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            HeldObject = obj;
            _transportableRb = rb;
        }
    }

    public void TryThrow(Vector3 direction, float strength = 2f)
    {
        if (HeldObject != null && _transportableRb != null)
        {
            strength = Mathf.Clamp(strength, 0, _maxThrowingStrength);
            direction = direction.normalized;
            _transportableRb.AddForce(direction * strength, ForceMode.Impulse);
            Drop();
        }
    }

    public void Drop()
    {
        HeldObject = null;
        _transportableRb = null;
    }

    private void HandleHolding()
    {
        if (HeldObject == null)
        {
            return;
        }
        if (HeldObject.gameObject == null)
        {
            HeldObject = null;
            return;
        }

        Vector3 displacement = _holdPoint.position - _transportableRb.position;
        Vector3 springForce = displacement * _holdStrength;
        Vector3 dampingForce = -_transportableRb.linearVelocity * _damping;

        Vector3 totalForce = springForce + dampingForce;
        totalForce = Vector3.ClampMagnitude(totalForce, _maxForce);

        _transportableRb.AddForce(totalForce, ForceMode.Force);
    }
}
