using UnityEngine;

public class GoToShop: BaseCustomerState
{
    public GoToShop(CustomerController controller) : base(controller)
    {
        
    }
    public override void EnterState()
    {
        //Debug.Log("Go to shop state entered.");
        _controller.Navigator.SetDestination(_controller.MapLocator.Shop.Locate().position);
    }

    public override void ExitState()
    {
        //Debug.Log("Go to shop state exited.");
    }

    public override void Execute()
    {
        if (Vector3.Distance(_controller.transform.position, _controller.MapLocator.Shop.Locate().position) <= _controller.Navigator.radius * 2)
        {
            _controller.OnChangeState(StateName.BUY);
        }
    }
}
