using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class MainView : View
{
    private readonly Dictionary<int, string> _wallets = new()
    {
        { 0, "USD"},
        { 1, "EUR"},
        { 2, "JPY"},
        { 3, "GBP"},
        { 4, "RUB"},
    };

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_InputField _outputField;

    [SerializeField] private TMP_Dropdown _firstWallet;
    [SerializeField] private TMP_Dropdown _secondWallet;


    [Inject] private readonly CurrencyCalculator _calculator;


    public override void Initialize()
    {
        _inputField.onValueChanged.AddListener(DisplayValue);
        _firstWallet.onValueChanged.AddListener((e) => DisplayValue(_inputField.text));
        _secondWallet.onValueChanged.AddListener((e) => DisplayValue(_inputField.text));
    }

    private void DisplayValue(string value)
    {
        if (value != string.Empty)
            _outputField.text = _calculator.CalculateCurrency(Convert.ToDouble(_inputField.text), _wallets[_firstWallet.value], _wallets[_secondWallet.value]).ToString();
    }
}
