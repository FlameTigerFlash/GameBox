using UnityEngine;

public enum WasteType {SMALL, BLACK, FOOD, PAPER, PLASTIC };
public class CollectibleWaste : BaseCollectible
{
    [SerializeField] private WasteType _type;

    [SerializeField] private int _costToRemove;

    public WasteType Type => _type;
    public int CostToRemove => _costToRemove;
}
