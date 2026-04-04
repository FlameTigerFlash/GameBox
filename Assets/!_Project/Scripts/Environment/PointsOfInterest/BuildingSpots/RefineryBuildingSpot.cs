using UnityEngine;
using Zenject;

public class RefineryBuildingSpot : MonoBehaviour
{
    [SerializeField] private GameObject _placeholder;
    [SerializeField] private GameObject _building;

    [SerializeField] private Transform _buildingSpot;

    [SerializeField] private int _cost = 50;

    public int Cost => _cost;

    private MapLocator _mapLocator;

    private void Start()
    {
        if (GameInfo.IsRefineryBuilt)
        {
            Build();
        }
    }

    [Inject]
    public void Construct(MapLocator mapLocator)
    {
        _mapLocator = mapLocator;
    }

    public void TryBuild()
    {
        if (GameInfo.Money < _cost)
        {
            return;
        }
        GameInfo.SubtractMoney(_cost);
        Build();
    }

    private void Build()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        GameObject refinery = Instantiate(_building, _buildingSpot.position, _buildingSpot.rotation, GameInfo.EnvironmentContainer != null ? GameInfo.EnvironmentContainer : transform.parent);
        _mapLocator.RegisterRefinery(refinery);
        if (_placeholder != null)
        {
            Destroy(_placeholder);
        }
    }
}
