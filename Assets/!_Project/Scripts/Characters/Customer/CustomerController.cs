using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using Zenject;

public enum StateName { GO_TO_SHOP, BUY, GO_TO_WASTE_BIN, GO_TO_BENCH, WAIT_NEAR_BENCH, LEAVE}

public class CustomerController : BaseController
{
    [SerializeField] private NavMeshAgent _navigator;

    [SerializeField] private MapLocator _mapLocator;

    [SerializeField] private WasteHandler _wasteHandler;

    [SerializeField] private HoldAbility _holdAbility;

    public NavMeshAgent Navigator => _navigator;
    public MapLocator MapLocator => _mapLocator;

    public WasteHandler WasteHandler => _wasteHandler;

    public HoldAbility HoldAbility => _holdAbility;

    private BaseCustomerState _currentState;

    private GoToShop _buyState;
    private LeaveState _leaveState;
    private WaitForFoodState _waitForFoodState;
    private GoToWasteBinState _goToWasteBinState;
    private GoToBenchState _goToBenchState;
    private WaitNearBenchState _waitNearBenchState;

    private void Start()
    {
        InitStates();
        ChangeState(_buyState);
    }

    private void FixedUpdate()
    {
        _currentState.Execute();
    }

    [Inject]
    public void Construct(MapLocator mapLocator)
    {
        _mapLocator = mapLocator;
    }

    public void OnChangeState(StateName stateName)
    {
        switch (stateName)
        {
            case StateName.GO_TO_SHOP:
                ChangeState(_buyState);
                break;
            case StateName.LEAVE:
                ChangeState(_leaveState);
                break;
            case StateName.BUY:
                ChangeState(_waitForFoodState);
                break;
            case StateName.GO_TO_WASTE_BIN:
                ChangeState(_goToWasteBinState);
                break;
            case StateName.WAIT_NEAR_BENCH:
                ChangeState(_waitNearBenchState);
                break;
            case StateName.GO_TO_BENCH:
                ChangeState(_goToBenchState);
                break;
        }
    }

    public void OnFinishJourney()
    {
        Debug.Log("Customer has left");
        Destroy(gameObject);
    }

    private void ChangeState(BaseCustomerState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState?.EnterState();
    }

    private void InitStates()
    {
        _buyState = new GoToShop(this);
        _leaveState = new LeaveState(this);
        _waitForFoodState = new WaitForFoodState(this);
        _goToWasteBinState = new GoToWasteBinState(this);
        _goToBenchState = new GoToBenchState(this);
        _waitNearBenchState = new WaitNearBenchState(this);
}

    public class Factory : PlaceholderFactory<CustomerController> { }
}
