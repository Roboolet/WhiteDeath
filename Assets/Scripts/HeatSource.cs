using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSource : MonoBehaviour
{
    [Header("Ticks")]
    public float tickWaitTime;
    float lastTick;

    [Header("Heat Settings")]
    public float intensity;
    public float range;
    [Range(0,0.01f)] public float falloff;

    Thermometer[] thermometers;

    protected virtual void Start()
    {
        thermometers = FindObjectsOfType<Thermometer>();
    }

    protected virtual void Update()
    {
        if (lastTick + tickWaitTime < Time.time)
        {
            lastTick = Time.time;
            Tick();
        }
    }

    protected virtual void Tick()
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
