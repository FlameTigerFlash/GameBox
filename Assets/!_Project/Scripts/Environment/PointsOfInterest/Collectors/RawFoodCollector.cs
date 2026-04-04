using UnityEngine;

public class RawFoodCollector : MonoBehaviour
{
    [SerializeField] private int _moneyReward = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<RawFoodCollectible>(out var food))
        {
            if (food.IsActive)
            {
                food.Collect();
                CollectFood();
            }
        }
    }

    private void CollectFood()
    {
        GameInfo.AddMoney(_moneyReward);
    }
}
