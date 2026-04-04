using UnityEngine;
using System.Collections;

public class JumpAbility : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private float _jumpCooldown = 0.5f;
    [SerializeField] private float _jumpAcceleration = 2f;

    private Rigidbody _rb;

    private bool _canJump = true;

    private void Awake()
    {
        _canJump = true;
        _rb = GetComponent<Rigidbody>();
    }

    public void TryJump()
    {
        if (_canJump && _groundChecker.CheckGrounded())
        {
            StartCoroutine(Cooldown(_jumpCooldown));
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpAcceleration, ForceMode.VelocityChange);
    }

    private IEnumerator Cooldown(float delay)
    {
        _canJump = false;
        yield return new WaitForSeconds(delay);
        _canJump = true;
    }
}
