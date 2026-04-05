using UnityEngine;

public class GoToShop: BaseCustomerState
{
    public GoToShop(CustomerController controller) : base(controller)
    {
        
    }
    public override void EnterState()
    {
        _controller.Navigator.SetDestination(_controller.MapLocator.Shop.Locate().position);
    }

    public override void ExitState()
    {
    }

    public override void Execute()
    {
        bool _isInRange = Vector3.Distance(_controller.transform.position, _controller.MapLocator.Shop.Locate().position) <= _controller.Navigator.radius * 2 + 1;
        if (_isInRange)
        {
            _controller.OnChangeState(StateName.BUY);
        }
    }
}
