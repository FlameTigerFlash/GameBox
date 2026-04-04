using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _currentTime = 0;
    public void Display(float time)
    {
        if (time < 0)
        {
            return;
        }
        if ((int)time != _currentTime)
        {
            _currentTime = (int)time;

            int minutes = _currentTime / 60, seconds = _currentTime % 60;
            string seconds_str = (seconds >= 10) ? $"{seconds}" : $"0{seconds}";

            _text.text = $"{minutes}:{seconds_str}";
        }
    }
}
