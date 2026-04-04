using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Tree : MonoBehaviour
{
    [SerializeField] private List<GameObject> _leaves;

    public void SetPolluted()
    {
        foreach (GameObject leaf in _leaves)
        {
            leaf.SetActive(false);
        }
    }

    public void SetClean()
    {
        foreach (GameObject leaf in _leaves)
        {
            leaf.SetActive(true);
        }
    }
}
