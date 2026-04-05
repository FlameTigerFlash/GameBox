using UnityEngine;

public abstract class BaseWasteBin : PointOfInterest
{
    [SerializeField] protected Transform _interactionPoint;
    [SerializeField] protected Transform _ejectionPoint;

    public abstract void LoadWaste();
    public override Transform Locate()
    {
        return _interactionPoint;
    }
}
