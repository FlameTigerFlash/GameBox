using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject _foodPrefab;

    [SerializeField] private Transform _center;

    [SerializeField] private Vector3 _scale;

    [SerializeField] private float _cooldown = 5f;

    [SerializeField] private int _maxAcceptablePollution = 75;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        StopAllCoroutines();
        StartCoroutine(Cooldown(_cooldown));
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void TrySpawnFood()
    {
        if (GameInfo.Pollution < _maxAcceptablePollution)
        {
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        Vector3 position = new Vector3(Random.Range(
            _center.position.x - _scale.x, _center.position.x + _scale.x),
            Random.Range(_center.position.y - _scale.y, _center.position.y + _scale.y),
            Random.Range(_center.position.z - _scale.z, _center.position.z + _scale.z));

        Instantiate(_foodPrefab, position, Quaternion.identity, GameInfo.Items != null? GameInfo.Items : transform);

        StartCoroutine(Cooldown(_cooldown));
    }

    private IEnumerator Cooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        TrySpawnFood();
    }
}
