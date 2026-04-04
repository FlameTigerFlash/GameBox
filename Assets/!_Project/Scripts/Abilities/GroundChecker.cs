using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _point;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _distance = 0.1f;

    public bool CheckGrounded()
    {
        return Physics.Raycast(_point.position, -_point.up, _distance, _layerMask);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_point.position, -_point.up * _distance);
    }
}
