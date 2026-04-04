using UnityEngine;

public abstract class BaseCollectible : MonoBehaviour
{
    public bool IsActive { get; private set; } = true;

    private void Start()
    {
        IsActive = true;
    }

    public virtual void Collect()
    {
        IsActive = false;
        Destroy(gameObject);
    }
}
