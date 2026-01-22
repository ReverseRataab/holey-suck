using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    void Start()
    {
        GameObject moneyObject = GameObject.Find("Money Text");
        moneyText = moneyObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        moneyText.text = $"{Currency.GetMoney()}$";
    }
}