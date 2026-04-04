using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _money = 0;

    public void Display(int money)
    {
        _money = money;
        _text.text = _money.ToString();
    }
}
