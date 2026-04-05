using UnityEngine;

public class MainMenuScreenManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _rulesCanvas;

    public void OnShowRules()
    {
        _mainMenuCanvas.enabled = false;
        _rulesCanvas.enabled = true;
    }

    public void OnShowMainMenu()
    {
        _mainMenuCanvas.enabled = true;
        _rulesCanvas.enabled = false;
    }
}
