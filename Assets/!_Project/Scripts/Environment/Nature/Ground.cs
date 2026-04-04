using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material _cleanMaterial;
    [SerializeField] private Material _pollutedMaterial;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetPolluted()
    {
        _renderer.material = _pollutedMaterial;
    }

    public void SetClean()
    {
        _renderer.material = _cleanMaterial;
    }
}
