using UnityEngine;

public class GoToWasteBinState : BaseCustomerState
{
    private BaseWasteBin _targetWasteBin;

    private HoldAbility _holdAbility;
    public GoToWasteBinState(CustomerController controller) : base(controller)
    {
        _holdAbility = controller.HoldAbility;
    }

    public override void EnterState()
    {
        //Debug.Log("Go to waste bin state entered.");
        if (!GameInfo.IsWasteBinBuilt && !GameInfo.IsSortingBinBuilt)
        {
            _controller.OnChangeState(StateName.LEAVE);
            return;
        }

        int wasteType = Random.Range(0, 2);
        if (wasteType == 0)
        {
            if (!GameInfo.IsWasteBinBuilt)
            {
                _controller.OnChangeState(StateName.LEAVE);
                return;
            }
            _targetWasteBin = _controller.MapLocator.WasteBin;
        }
        else
        {
            if (GameInfo.IsSortingBinBuilt)
            {
                _targetWasteBin = _controller.MapLocator.SortingBin;
            }
            else if (GameInfo.IsWasteBinBuilt)
            {
                _targetWasteBin = _controller.MapLocator.WasteBin;
            }
            else
            {
                _controller.OnChangeState(StateName.LEAVE);
                return;
            }
        }

        if (_targetWasteBin != null)
        {
            _controller.Navigator.SetDestination(_targetWasteBin.Locate().position);
        }
    }

    public override void Execute()
    {
        if (Vector3.Distance(_controller.transform.position, _targetWasteBin.Locate().position) <= 1f)
        {
            _holdAbility.Drop();
            _controller.OnChangeState(StateName.LEAVE);
        }
    }

    public override void ExitState()
    {
        //Debug.Log("Go to waste bin state exited.");
    }
}
