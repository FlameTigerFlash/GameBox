using UnityEngine;

public class WasteHandler : MonoBehaviour
{
    [SerializeField] private GameObject _smallWastePrefab;

    public GameObject SpawnWaste(Transform point)
    {
        GameObject waste = Instantiate(_smallWastePrefab, point.position, Quaternion.identity, GameInfo.Items != null ? GameInfo.Items : transform.parent);
        return waste;
    }
}
