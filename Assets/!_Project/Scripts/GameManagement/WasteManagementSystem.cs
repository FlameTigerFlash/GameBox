using System.Collections;
using UnityEngine;

public class WasteManagementSystem : MonoBehaviour
{
    [SerializeField] private TruckController _truckController;
    [SerializeField] private WasteAreaCollector _wasteAreaCollector;

    [SerializeField] private float _pollutionDecreaseTime= 2f;
    [SerializeField] private float _truckCooldown = 30f;

    [SerializeField] private int _pollutionDecreaseAmount = 1;

    public void Launch()
    {
        StopAllCoroutines();
        StartCoroutine(TruckCooldown(_truckCooldown));
        StartCoroutine(PollutionDecreaseCooldown(_pollutionDecreaseAmount));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void OnTruckFinishedJourney()
    {
        StartCoroutine(TruckCooldown(_truckCooldown));
    }

    public void OnTruckFinishedService()
    {
        _wasteAreaCollector.CollectAllWaste();
    }

    private void SendTruck()
    {
        _truckController.gameObject.SetActive(true);
        _truckController.StartJourney();
    }

    private void DecreasePollution()
    {
        GameInfo.SubtractPollution(_pollutionDecreaseAmount);
        StartCoroutine(PollutionDecreaseCooldown(_pollutionDecreaseAmount));
    }

    private IEnumerator TruckCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        SendTruck();
    }

    private IEnumerator PollutionDecreaseCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        DecreasePollution();
    }
}
