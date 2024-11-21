using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyCalculator : MonoBehaviour
{
    private Dictionary<string, double> _currency = new();
    public void UpdateData(Dictionary<string, double> data)
    {
        _currency = new(data);
    }

    public double CalculateCurrency(double amount, string from, string to)
    {
        if (from == to)
            return amount;
        if (_currency.ContainsKey(from + to))
        {
            return Math.Round(amount * Convert.ToDouble(_currency[from + to]), 3);
        }
        else if (_currency.ContainsKey(to + from))
        {
            return Math.Round(amount * (1d / Convert.ToDouble(_currency[to + from])), 3);
        }
        foreach (var intermediateCurrency in new[] { "USD", "EUR", "JPY", "GBP", "RUB" })
        {
            string firstPair = from + intermediateCurrency;
            string secondPair = intermediateCurrency + to;

            if (_currency.ContainsKey(firstPair) && _currency.ContainsKey(secondPair))
            {
                double intermediateAmount = amount * Convert.ToDouble(_currency[firstPair]);
                return Math.Round(intermediateAmount * Convert.ToDouble(_currency[secondPair]), 3);
            }
        }
        throw new InvalidOperationException($"Конвертация из {to} в {from} невозможна с использованием доступных пар.");
    }
}
