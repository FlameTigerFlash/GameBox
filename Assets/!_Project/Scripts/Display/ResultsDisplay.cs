using TMPro;
using UnityEngine;

public class ResultsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyValue;
    [SerializeField] private TMP_Text _moneyIncrement;
    [SerializeField] private TMP_Text _pollutionValue;
    [SerializeField] private TMP_Text _pollutionIncrement;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisplayMoneyInfo();
        DisplayPollutionInfo();
    }

    private void DisplayMoneyInfo()
    {
        _moneyValue.text = $"{GameInfo.Money}";

        if (GameInfo.Money - GameInfo.InitialMoney > 0)
        {
            _moneyIncrement.text = $"+{(GameInfo.Money - GameInfo.InitialMoney)}";
            _moneyIncrement.color = Color.green;
        }
        else if (GameInfo.Money - GameInfo.InitialMoney < 0)
        {
            _moneyIncrement.text = $"{(GameInfo.Money - GameInfo.InitialMoney)}";
            _moneyIncrement.color = Color.red;
        }
        else
        {
            _moneyIncrement.text = "0";
            _moneyIncrement.color = Color.white;
        }
    }

    private void DisplayPollutionInfo()
    {
        _pollutionValue.text = $"{GameInfo.Pollution}%";

        if (GameInfo.Pollution - GameInfo.InitialPollution > 0)
        {
            _pollutionIncrement.text = $"+{(GameInfo.Pollution - GameInfo.InitialPollution)}%";
            _pollutionIncrement.color = Color.red;
        }
        else if (GameInfo.Pollution - GameInfo.InitialPollution < 0)
        {
            _pollutionIncrement.text = $"{(GameInfo.Pollution - GameInfo.InitialPollution)}%";
            _pollutionIncrement.color = Color.green;
        }
        else
        {
            _pollutionIncrement.text = "0%";
            _pollutionIncrement.color = Color.white;
        }
    }
}
