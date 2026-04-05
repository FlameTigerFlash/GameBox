using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WaitNearBenchState : BaseCustomerState
{
    private bool _hasRested = false;
    public WaitNearBenchState(CustomerController controller) : base(controller)
    {

    }
    public override void EnterState()
    {
        _controller.StartCoroutine(Cooldown(5f));
    }

    public override void ExitState()
    {
    }

    public override void Execute()
    {
        if (_hasRested)
        {
            Finish();
        }
    }

    private void Finish()
    {
        _controller.OnChangeState(StateName.LEAVE);
    }

    private IEnumerator Cooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        _hasRested = true;
    }
}
