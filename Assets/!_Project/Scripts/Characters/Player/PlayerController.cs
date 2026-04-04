using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMove _characterMove;
    [SerializeField] private JumpAbility _jumpAbility;
    [SerializeField] private LookRotate _lookRotate;
    [SerializeField] private PlayerInteract _playerInteract;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _characterMove.Move(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpAbility.TryJump();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _lookRotate.SetLookInput(direction);

        _playerInteract.TryReadDescription();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInteract.TryInteract();
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInteract.TryPickUp();
        }
    }

    public void OnRelease(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInteract.TryDrop();
        }
    }
}
