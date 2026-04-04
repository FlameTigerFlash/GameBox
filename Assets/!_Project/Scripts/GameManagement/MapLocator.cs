using UnityEngine;

public class MapLocator : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private PointOfInterest _exit;
    [SerializeField] private PointOfInterest _bench;

    public Shop Shop => _shop;
    public PointOfInterest Exit => _exit;

    public PointOfInterest Bench => _bench;

    public WasteBin WasteBin {get; private set;}

    public SortingBin SortingBin { get; private set;}

    public void RegisterWasteBin(WasteBin bin)
    {
        WasteBin = bin;
        GameInfo.RegisterWasteBin();
    }

    public void RegisterSortingBin(SortingBin bin)
    {
        SortingBin = bin;
        GameInfo.RegisterSortingBin();
    }

    public void RegisterRefinery(GameObject refinery)
    {
        GameInfo.RegisterRefinery();
    }
}
