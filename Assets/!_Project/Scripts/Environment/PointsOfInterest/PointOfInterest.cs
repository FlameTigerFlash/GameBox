using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField] private Transform _point;
    public virtual Transform Locate()
    {
        return _point;
    }
}
