using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : MonoBehaviour
{
    [SerializeField] float startTemperature, temperatureMax, temperatureMin;

    [SerializeField] float entropy;
    public float entropyMultShelter { get; set; }
    public float entropyMultWeather { get; set; }

    float _Temperature;
    public float Temperature
    {
        get { return _Temperature; }
        private set
        {
            _Temperature = Mathf.Clamp(value, temperatureMin, temperatureMax);
            if (value < temperatureMin) OnExceedMinTemp();
        }
    }

    public float NormalizedColdness => 1 - (_Temperature - temperatureMin) / (temperatureMax - temperatureMin);

    private void Start()
    {
        Temperature = startTemperature;
    }

    private void Update()
    {
        Temperature -= entropy * entropyMultShelter * entropyMultWeather * Time.deltaTime;
    }

    public void AddTemperature(float amount)
    {
        Temperature += amount;
    }

    protected virtual void OnExceedMinTemp()
    {

    }
}
