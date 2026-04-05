using UnityEngine;

public class GoToBenchState : BaseCustomerState
{
    public GoToBenchState(CustomerController controller) : base(controller)
    {

    }
    public override void EnterState()
    {
        if (_controller.MapLocator.Bench == null || GameInfo.Pollution > 50)
        {
            Finish();
        }
        _controller.Navigator.SetDestination(_controller.MapLocator.Bench.Locate().position);
    }

    public override void ExitState()
    {
    }

    public override void Execute()
    {
        if (Vector3.Distance(_controller.transform.position, _controller.MapLocator.Bench.Locate().position) <= 1.5f)
        {
            Finish();
        }
    }

    private void Finish()
    {
        _controller.OnChangeState(StateName.WAIT_NEAR_BENCH);
    }
}
