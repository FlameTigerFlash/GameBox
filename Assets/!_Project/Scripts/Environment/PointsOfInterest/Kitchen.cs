using System.Collections;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    [SerializeField] private float _cookingTime = 4.5f;
    [SerializeField] private int _cookingPollution = 5;

    [SerializeField] private int _maxFood = 5;

    private int _currentFood = 0;

    private void Start()
    {
        StartCoroutine(CookingProcess(_cookingTime));
    }

    public void TryCook()
    {
        Cook();
        StartCoroutine(CookingProcess(_cookingTime));
    }

    private void Cook()
    {
        Debug.Log("Cooking");
        if (_currentFood >= _maxFood)
        {
            return;
        }
        _currentFood += 1;

        while (_shop.HasFreeSlot() && _currentFood > 0)
        {
            _currentFood--;
            _shop.TryAddFood();
        }

        if (!GameInfo.IsRefineryBuilt)
        {
            GameInfo.AddPollution(_cookingPollution);
        }
    }

    private IEnumerator CookingProcess(float delay)
    {
        yield return new WaitForSeconds(delay);
        TryCook();
    }
}
