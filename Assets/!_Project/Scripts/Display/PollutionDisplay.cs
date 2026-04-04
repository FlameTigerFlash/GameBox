using TMPro;
using UnityEngine;

public class PollutionDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _pollution = 0;

    public void Display(int pollution)
    {
        _pollution = pollution;
        _text.text = _pollution.ToString();
    }
}
