using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName = "MainMenu";
    [SerializeField] private string _gameLevelSceneName = "GameLevel";
    [SerializeField] private string _resultsScreenSceneName = "ResultsScreen";

    public void OnMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void OnGameLevel()
    {
        SceneManager.LoadScene(_gameLevelSceneName);
    }

    public void OnResultsScreen()
    {
        SceneManager.LoadScene(_resultsScreenSceneName);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
