using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowstorm : MonoBehaviour
{
    [SerializeField] float secondsUntilStorm;
    [Space]
    [SerializeField] float particleRate;
    [SerializeField] float particleLifetime, wind;
    [SerializeField] ParticleSystem ps_white, ps_black;
    [SerializeField] ParticleSystemForceField ps_forcefield;

    bool started;

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (!started)
        {
            started = true;
            StartCoroutine("StormCountdown");
        }
    }

    IEnumerator StormCountdown()
    {
        yield return new WaitForSeconds(secondsUntilStorm);
        var emissionW = ps_white.emission;
        var emissionB = ps_black.emission;

        emissionW.rateOverTimeMultiplier = particleRate;
        emissionB.rateOverTimeMultiplier = particleRate;

        var mainW = ps_white.main;
        var mainB = ps_black.main;

        mainW.startLifetime = particleLifetime;
        mainB.startLifetime = particleLifetime;

        ps_forcefield.directionX = wind;
    }
}
