using System.Collections;
using UnityEngine;
using Zenject;

public class CustomerSpawner : MonoBehaviour
{
    [Inject] private CustomerController.Factory _customerFactory;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _destination;

    [SerializeField] private float _cooldown = 3f;

    private bool _canSpawn = true;

    private void Start()
    {
        _canSpawn = true;
    }

    public void TrySpawnCustomer()
    {
        if (_canSpawn)
        {
            SpawnCustomer();
            _canSpawn = false;
            StartCoroutine(Cooldown(_cooldown));
        }
    }

    private void SpawnCustomer()
    {
        CustomerController customer = _customerFactory.Create();
        if (GameInfo.NPC != null)
        {
            customer.transform.SetParent(GameInfo.NPC);
        }
        customer.transform.position = _spawnPoint.position;
    }

    private IEnumerator Cooldown(float delay)
    {
        _canSpawn = false;
        yield return new WaitForSeconds(delay);
        _canSpawn = true;
    }
}
