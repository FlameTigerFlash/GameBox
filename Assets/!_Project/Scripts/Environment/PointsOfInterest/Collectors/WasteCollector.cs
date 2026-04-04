using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class WasteCollector : MonoBehaviour
{
    [SerializeField] private List<WasteType> _acceptedWasteTypes;

    public UnityEvent WasteCollectEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CollectibleWaste>(out var waste))
        {
            if (waste.IsActive && CanCollect(waste))
            {
                waste.Collect();
                WasteCollectEvent.Invoke();
            }
        }
    }

    private bool CanCollect(CollectibleWaste waste)
    {
        return _acceptedWasteTypes.Contains(waste.Type);
    }
}
