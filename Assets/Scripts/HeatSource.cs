using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSource : MonoBehaviour
{
    [Header("Ticks")]
    [SerializeField] float tickWaitTime;
    float lastTick;

    [Header("Heat Settings")]
    public float intensity;
    public float range;
    [Range(0,0.01f)] public float falloff;

    Thermometer[] thermometers;

    private void Start()
    {
        thermometers = FindObjectsOfType<Thermometer>();
    }

    private void Update()
    {
        if (lastTick + tickWaitTime < Time.time)
        {
            lastTick = Time.time;
            Tick();
        }
    }

    void Tick()
    {
        for(int i = 0; i < thermometers.Length; i++)
        {
            Thermometer t = thermometers[i];
            float dist = Vector2.Distance(transform.position, t.transform.position);

            if (dist <= range)
            {
                t.AddTemperature(intensity);                
            }
            else
            {
                t.AddTemperature(Mathf.Max(0, intensity - Mathf.Pow(dist - intensity, 3) * falloff));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
