using System.Collections;
using UnityEngine;

public class Decompose : MonoBehaviour
{
    [SerializeField] private float _delay = 5.0f;

    [SerializeField] private int _pollutionAmount = 10;

    private void Start()
    {
        StartCoroutine(DecomposeProcess(_delay));
    }

    public void StopDecomposition()
    {
        StopAllCoroutines();
    }

    public void StartDecomposition()
    {
        StartCoroutine(DecomposeProcess(_delay));
    }

    private void FinishDecomposition()
    {
        GameInfo.AddPollution(_pollutionAmount);
        Destroy(gameObject);
    }

    private IEnumerator DecomposeProcess(float delay)
    {
        yield return new WaitForSeconds(delay);
        FinishDecomposition();
    }
}
