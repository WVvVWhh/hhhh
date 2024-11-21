using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using Newtonsoft.Json;

public class WebRequestsHandler : MonoBehaviour
{
    [Inject] private readonly CurrencyCalculator _calculator;
    private const int UPDATE_RATE = 120;
    private const string API = "a0c5bda355662bfd5f54cb10413dfb9a";
    private List<string> _pairs;

    private void Awake()
    {
        _pairs = new()
        {
            "USDEUR",
            "USDJPY",
            "USDGBP",
            "USDRUB",
            "EURJPY",
            "EURGBP",
            "EURRUB",
            "JPYGBP",
            "JPYRUB",
            "GBPRUB"
        };
        StartCoroutine(UpdateCurrency());

    }

    private IEnumerator UpdateCurrency()
    {
        using UnityWebRequest www = UnityWebRequest.Get($"https://currate.ru/api/?get=rates&pairs={string.Join(",", _pairs)}&key={API}");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log("pisyn yayichkin");
        else
        {
            RatesResponce responce = JsonConvert.DeserializeObject<RatesResponce>(www.downloadHandler.text);
            Debug.Log(JsonConvert.SerializeObject(new RatesResponce() { data = new Dictionary<string, double>() { { "SD", 12 } } }));
            Debug.Log(www.downloadHandler.text);
            Debug.Log(responce.data);
            _calculator.UpdateData(responce.data);
        }
        yield return new WaitForSeconds(UPDATE_RATE);
        StartCoroutine(UpdateCurrency());
    }
}

[Serializable]
public class RatesResponce
{
    public string status;
    public string message;
    public Dictionary<string, double> data;
}
