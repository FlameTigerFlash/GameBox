using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private PlayerDisplay _playerDisplay;

    [SerializeField] private HoldAbility _holdAbility;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float interactDistance = 3f;

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
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, interactDistance, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.TryGetComponent<TransportableItem>(out var transportableItem))
            {
                _holdAbility.TryPickUp(hit.collider.gameObject);
            }
        }
    }

    public void TryDrop()
    {
        _holdAbility.Drop();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * interactDistance, Color.yellow);
    }
}
