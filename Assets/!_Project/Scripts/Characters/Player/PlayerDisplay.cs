using TMPro;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _midTextField;
    public void DisplayOnMid(string text)
    {
        if (_midTextField != null)
        {
            _midTextField.text = text;
        }
    }

    public void ClearAll()
    {
        _midTextField.text = "";
    }
}
