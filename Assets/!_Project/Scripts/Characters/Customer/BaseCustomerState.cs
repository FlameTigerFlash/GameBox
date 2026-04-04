using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCustomerState
{
    protected CustomerController _controller;

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void Execute();

    public BaseCustomerState(CustomerController controller)
    {
        _controller = controller;
    }
}
