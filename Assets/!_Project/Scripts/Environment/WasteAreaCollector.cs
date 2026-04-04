using System.Collections.Generic;
using UnityEngine;

public class WasteAreaCollector : MonoBehaviour
{
    private HashSet<CollectibleWaste> _wastesInZone = new HashSet<CollectibleWaste>();

    private void OnTriggerEnter(Collider other)
    {
        var waste = other.GetComponent<CollectibleWaste>();
        if (waste != null && !_wastesInZone.Contains(waste))
        {
            _wastesInZone.Add(waste);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var waste = other.GetComponent<CollectibleWaste>();
        if (waste != null && _wastesInZone.Contains(waste))
        {
            _wastesInZone.Remove(waste);
        }
    }

    public void CollectAllWaste()
    {
        int fine = 0;
        var wastesToCollect = new List<CollectibleWaste>(_wastesInZone);

        foreach (var waste in wastesToCollect)
        {
            if (waste == null)
            {
                continue;
            }
            fine += waste.CostToRemove;
            waste.Collect();
        }

        _wastesInZone.Clear();
        GameInfo.SubtractMoney(fine);
    }
}