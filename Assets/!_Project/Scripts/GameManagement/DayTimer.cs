using UnityEngine;
using UnityEngine.Events;

public class DayTimer : MonoBehaviour
{
    [SerializeField] private TimeDisplay _display;

    [SerializeField] private float _roundTime = 180f;

    public UnityEvent TimeOverEvent;

    public float TimeLeft { get; private set; } = 5f;

    private bool _isActive = false;

    public void StartTimer()
    {
        TimeLeft = _roundTime;
        _isActive = true;
    }

    public void StopTimer()
    {
        _isActive = false;
    }

    private void Update()
    {
        if (_isActive)
        {
            TimeLeft -= Time.deltaTime;
            _display.Display(TimeLeft);
        }
        if (TimeLeft <= 0)
        {
            TimeOverEvent.Invoke();
            StopTimer();
        }
    }
}
