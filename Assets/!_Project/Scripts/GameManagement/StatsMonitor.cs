using UnityEngine;

public class StatsMonitor : MonoBehaviour
{
    [SerializeField] private MoneyDisplay _moneyDisplay;
    [SerializeField] private PollutionDisplay _pollutionDisplay;

    [SerializeField] private Ground _ground;
    [SerializeField] private Tree _tree;
    [SerializeField] private GameObject _squirrel;

    [SerializeField] private int _groundMaxAcceptablePollution = 75;
    [SerializeField] private int _treeMaxAcceptablePollution = 50;
    [SerializeField] private int _squirrelMaxAcceptablePollution = 50;

    private void Awake()
    {
        GameInfo.MoneyChangedEvent.AddListener(OnMoneyChange);
        GameInfo.PollutionChangedEvent.AddListener(OnPollutionChange);
    }

    private void Start()
    {
        Refresh();
    }

    public void OnMoneyChange(int money)
    {
        _moneyDisplay.Display(money);
    }

    public void OnPollutionChange(int pollution)
    {
        _pollutionDisplay.Display(pollution);
        HandleEnvironmentPollution();
    }

    public void Refresh()
    {
        _moneyDisplay.Display(GameInfo.Money);
        _pollutionDisplay.Display(GameInfo.Pollution);
    }

    private void HandleEnvironmentPollution()
    {
        if (GameInfo.Pollution >= _groundMaxAcceptablePollution)
        {
            _ground.SetPolluted();
        }
        else
        {
            _ground.SetClean();
        }
        if (GameInfo.Pollution >= _treeMaxAcceptablePollution)
        {
            _tree.SetPolluted();
        }
        else
        {
            _tree.SetClean();
        }
        if (GameInfo.Pollution >= _squirrelMaxAcceptablePollution)
        {
            _squirrel.SetActive(false);
        }
        else
        {
            _squirrel.SetActive(true);
        }
    }
}
