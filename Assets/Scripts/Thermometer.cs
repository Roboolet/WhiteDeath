using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : MonoBehaviour
{
    [SerializeField] float startTemperature, temperatureMax, temperatureMin;

    [SerializeField] float entropy;
    [HideInInspector] public float entropyMult = 1;

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

    private void Start()
    {
        Temperature = startTemperature;
    }

    private void Update()
    {
        Temperature -= entropy * entropyMult * Time.deltaTime;
    }

    public void AddTemperature(float amount)
    {
        Temperature += amount;
    }

    protected virtual void OnExceedMinTemp()
    {

    }
}
