using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Rigidbody _rb;

    private Vector2 _moveDirection = Vector2.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        _rb.linearVelocity = right * _moveDirection.x * _speed + up * _rb.linearVelocity.y + forward *_moveDirection.y * _speed;
    }

    public void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }
}
