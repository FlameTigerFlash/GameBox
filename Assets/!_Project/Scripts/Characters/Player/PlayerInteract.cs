using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private PlayerDisplay _playerDisplay;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Transform _holdPoint;

    [SerializeField] private float _springStrength = 20f;
    [SerializeField] private float _damping = 5f;
    [SerializeField] private float _maxForce = 50f;
    [SerializeField] private float interactDistance = 3f;

    private TransportableItem _carriedItem;
    private Rigidbody _transportableRb;

    private void FixedUpdate()
    {
        HandleHolding();
    }

    public void TryReadDescription()
    {
        bool found = false;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, interactDistance, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<InteractiveObject>(out var interactiveObject))
            {
                found = true;
                _playerDisplay.DisplayOnMid(interactiveObject.Descriptiion);
            }
        }
        if (!found)
        {
            _playerDisplay.ClearAll();
        }
    }

    public void TryInteract()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, interactDistance, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<InteractiveObject>(out var interactiveObject))
            {
                interactiveObject.Interact();
            }
        }
    }

    public void TryPickUp()
    {
        if (_carriedItem != null)
        {
            return;
        }
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, interactDistance, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<TransportableItem>(out var transportableItem))
            {
                HoldItem(transportableItem);
            }
        }
    }

    public void TryDrop()
    {
        if (_carriedItem != null)
        {
            DropItem();
        }
    }

    private void HoldItem(TransportableItem item)
    {
        _carriedItem = item;
        item.PickUp(_holdPoint);
        _transportableRb = item.GetComponent<Rigidbody>();
    }

    private void DropItem()
    {
        _carriedItem.Drop();
        _carriedItem = null;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * interactDistance, Color.yellow);
    }

    private void HandleHolding()
    {
        if (_carriedItem == null)
        {
            return;
        }
        if (_carriedItem.gameObject == null)
        {
            _carriedItem = null;
            return;
        }

        if (Vector3.Distance(_carriedItem.transform.position, _holdPoint.position) > interactDistance)
        {
            DropItem();
            return;
        }

        bool contact = Physics.Raycast(_camera.transform.position,_carriedItem.transform.position - _camera.transform.position, out var hit, interactDistance, _layerMask, QueryTriggerInteraction.Collide);
        if (contact && (hit.collider.gameObject != _carriedItem.gameObject))
        {
            DropItem();
            return;
        }

        Vector3 displacement = _holdPoint.position - _transportableRb.position;
        Vector3 springForce = displacement * _springStrength;
        Vector3 dampingForce = -_transportableRb.linearVelocity * _damping;

        Vector3 totalForce = springForce + dampingForce;
        totalForce = Vector3.ClampMagnitude(totalForce, _maxForce);

        _transportableRb.AddForce(totalForce, ForceMode.Force);
    }
}
