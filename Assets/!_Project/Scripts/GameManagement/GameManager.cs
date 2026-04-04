using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameManager : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private WasteManagementSystem _wasteManagementSystem;

    [SerializeField] private MapLocator _mapLocator;

    [SerializeField] private DayTimer _timer;

    [SerializeField] private GameObject _customerPrefab;

    private void Awake()
    {
        GameInfo.SetInfo();
    }

    private void Start()
    {
        _wasteManagementSystem.Launch();
        _timer.StartTimer();
    }

    public override void InstallBindings()
    {
        Container.Bind<MapLocator>().FromInstance(_mapLocator).AsSingle();
        Container.BindFactory<CustomerController, CustomerController.Factory>()
            .FromComponentInNewPrefab(_customerPrefab);
    }

    public void OnTimeOver()
    {
        EndDay();
    }

    private void EndDay()
    {
        GameInfo.WriteStats();
        _sceneLoader.OnResultsScreen();
    }
}
