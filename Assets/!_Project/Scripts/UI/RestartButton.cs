using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private SceneLoader _loader;

    public void OnButtonPressed()
    {
        GameInfo.ResetStats();
        _loader.OnGameLevel();
    }
}
