using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class LeaveState : BaseCustomerState
{
    private MapLocator _mapLocator;

    private NavMeshAgent _navigator;

    private HoldAbility _holdAbility;

    private WasteHandler _wasteHandler;

    private Vector3 _destination;

    private float _throwDistance = 0;

    public LeaveState(CustomerController controller) : base(controller)
    {
        _navigator = _controller.Navigator;
        _mapLocator = _controller.MapLocator;
        _holdAbility = _controller.HoldAbility;
        _wasteHandler = _controller.WasteHandler;
    }

    public override void EnterState()
    {
        //Debug.Log("Leave state entered.");
        _destination = _mapLocator.Exit.Locate().position;
        _navigator.SetDestination(_destination);
        if (_holdAbility.HeldObject != null)
        {
            _throwDistance = Random.Range(2f, Vector3.Distance(_controller.transform.position, _destination));
        }
    }

    public override void ExitState()
    {
        //Debug.Log("Leave state exited.");
    }

    public override void Execute()
    {
        if (_holdAbility.HeldObject != null && Vector3.Distance(_controller.transform.position, _destination) <= _throwDistance)
        {
            ConsumeFood();
        }
        if (Vector3.Distance(_controller.transform.position, _destination) <= 1)
        {
            ChooseNextAction();
        }
    }

    private void ConsumeFood()
    {
        GameObject foodBox = _holdAbility.HeldObject;
        _holdAbility.Drop();
        MonoBehaviour.Destroy(foodBox);

        GameObject waste = _wasteHandler.SpawnWaste(_holdAbility.HoldPoint);
        _holdAbility.TryPickUp(waste);

        if (GameInfo.IsWasteBinBuilt || GameInfo.IsSortingBinBuilt)
        {
            _controller.OnChangeState(StateName.GO_TO_WASTE_BIN);
            waste.GetComponent<Decompose>().StopDecomposition();
            return;
        }
        else
        {
            ThrowWaste();
        }
    }

    private void ThrowWaste()
    {
        Vector3 direction = Vector3.zero;
        float rnd = Random.Range(0, 1);

        if (rnd >= 0.5)
        {
            direction = _holdAbility.HoldPoint.right;
        }
        else
        {
            direction  = - _holdAbility.HoldPoint.right;
        }
        _holdAbility.TryThrow(direction);
    }

    private void ChooseNextAction()
    {
        float rnd = Random.Range(0f, 1f);
        if (GameInfo.Pollution < 50 && rnd > 0.5)
        {
            _controller.OnChangeState(StateName.GO_TO_SHOP);
        }
        else
        {
            _controller.OnFinishJourney();
        }
    }
}
