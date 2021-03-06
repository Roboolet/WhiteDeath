using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

[RequireComponent(typeof(HeatSource))]
public class Fireplace : Interactable
{
    [Header("Fireplace settings")]
    [SerializeField] float maxTimeLeft;
    [SerializeField] float minHeatRate;
    float timeLeft;

    HeatSource heatSource;
    [SerializeField] ParticleSystem flames;
    ParticleSystem.EmissionModule flames_emission;
    float flames_emissionOriginalROT;

    protected override void Awake()
    {
        base.Awake();
        heatSource = GetComponent<HeatSource>();

        flames_emission = flames.emission;
        flames_emissionOriginalROT = flames_emission.rateOverTimeMultiplier;
    }

    protected override void OnInsert(Item item)
    {
        base.OnInsert(item);
        Burnable b = ((Burnable)item);
        timeLeft = Mathf.Clamp(timeLeft + b.burnTime, 0, maxTimeLeft);

        flames.gameObject.SetActive(true);
        flames.Play();
        heatSource.isEmitting = true;
        SFXLib.current.Play("burn");
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            float normalizedBurntime = timeLeft / maxTimeLeft;
            var shape = flames.shape;
            var VOT = flames.velocityOverLifetime;

            VOT.yMultiplier = normalizedBurntime;
            VOT.orbitalYMultiplier = normalizedBurntime;
            flames_emission.rateOverTimeMultiplier = flames_emissionOriginalROT * normalizedBurntime;
            shape.radius = normalizedBurntime + 0.2f;

            heatSource.tickWaitTimeMultiplier = Mathf.Max(minHeatRate, 1 - normalizedBurntime);

            timeLeft -= Time.deltaTime;
        }
        else
        {
            if (flames.isEmitting) flames.Stop();
            if (heatSource.isEmitting) heatSource.isEmitting = false;
        }
    }
}
