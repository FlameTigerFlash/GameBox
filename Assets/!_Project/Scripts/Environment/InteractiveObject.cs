using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private string _description = "[E] to interact";

    public UnityEvent InteractionEvent;

    public string Descriptiion => _description;

    public void Interact()
    {
        InteractionEvent.Invoke();
    }
}
