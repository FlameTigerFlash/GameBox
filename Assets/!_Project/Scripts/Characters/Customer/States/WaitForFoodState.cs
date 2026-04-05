using UnityEngine;

public class WaitForFoodState : BaseCustomerState
{

    private Shop _shop;

    private HoldAbility _holdAbility;
    public WaitForFoodState(CustomerController controller) : base(controller)
    {
        _shop = controller.MapLocator.Shop;
        _holdAbility = controller.HoldAbility;
    }
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void Execute()
    {
        if (_shop.CurrentFood <= 0)
        {
            return;
        }
        if (_shop.TryBuyFood(_holdAbility.HoldPoint, out var food))
        {
            _holdAbility.TryPickUp(food);
            Finish();
        }
    }

    private void Finish()
    {
        if (_controller.MapLocator.Bench != null && GameInfo.Pollution < 50)
        {
            _controller.OnChangeState(StateName.GO_TO_BENCH);
            return;
        }
        _controller.OnChangeState(StateName.LEAVE);
    }
}
