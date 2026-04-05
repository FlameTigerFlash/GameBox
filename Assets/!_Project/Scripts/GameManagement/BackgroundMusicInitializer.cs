using UnityEngine;

public class BackgroundMusicInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundMusicPrefab;

    private void Awake()
    {
        if (GameObject.Find("BackgroundMusic") == null)
        {
            GameObject instance = Instantiate(_backgroundMusicPrefab);
            instance.name = "BackgroundMusic";
            DontDestroyOnLoad(instance);
        }
    }

}
