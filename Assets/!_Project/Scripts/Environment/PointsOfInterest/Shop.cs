using System.Collections;
using UnityEngine;

public class Shop : PointOfInterest
{
    [SerializeField] private GameObject _foodPrefab;

    [SerializeField] private int _maxFood = 3;
    [SerializeField] private int _foodPrice = 3;

    public int CurrentFood => _currentFood;

    private int _currentFood = 0;

    public bool TryBuyFood(Transform _holdPoint, out GameObject foodBox)
    {
        foodBox = null;
        if (_currentFood <= 0)
        {
            return false;
        }
        _currentFood -= 1;
        GameInfo.AddMoney(_foodPrice);

        foodBox = Instantiate(_foodPrefab, _holdPoint.position, Quaternion.identity, GameInfo.Items);
        return true;
    }

    public bool TryAddFood()
    {
        if (_currentFood < _maxFood)
        {
            _currentFood++;
            return true;
        }
        return false;
    }

    public bool HasFreeSlot()
    {
        return _currentFood < _maxFood;
    }
}
