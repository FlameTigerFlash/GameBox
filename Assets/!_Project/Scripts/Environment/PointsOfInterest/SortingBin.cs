using UnityEditor;
using UnityEngine;

public class SortingBin : BaseWasteBin
{
    [SerializeField] private GameObject _plasticWastePrefab;
    [SerializeField] private GameObject _foodWastePrefab;
    [SerializeField] private GameObject _paperWastePrefab;

    [SerializeField] private int _capacity = 5;

    private int _currentLoad = 0;

    public override void LoadWaste()
    {
        _currentLoad += 1;
        if (_currentLoad >= _capacity)
        {
            EjectContent();
        }
    }

    private void EjectContent()
    {
        _currentLoad = 0;
        int choice = Random.Range(0, 3);

        GameObject waste;
        switch (choice)
        {
            case 0:
                waste = _foodWastePrefab;
                break;
            case 1:
                waste = _plasticWastePrefab;
                break;
            default:
                waste = _paperWastePrefab;
                break;
        }
        Instantiate(waste, _ejectionPoint.position, Quaternion.identity, GameInfo.Items != null ? GameInfo.Items : transform);
    }
}
