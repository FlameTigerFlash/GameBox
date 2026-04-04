using UnityEngine;

public class WasteBin : BaseWasteBin
{
    [SerializeField] private GameObject _blackBagPrefab;

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
        Instantiate(_blackBagPrefab, _ejectionPoint.position, Quaternion.identity, GameInfo.Items != null? GameInfo.Items: transform);
    }
}
