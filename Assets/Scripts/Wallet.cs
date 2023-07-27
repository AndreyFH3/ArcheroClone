using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money;
    private int Money { get => _money; set { _money = value; _moneyText.text = $"Coins: {_money}"; } }
    [SerializeField] private TextMeshProUGUI _moneyText;


    private void OnEnable()
    {
        Money = PlayerPrefs.GetInt("Coins", 0);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Coins", Money);
    }

    public void AddCoin() => Money++;

}
