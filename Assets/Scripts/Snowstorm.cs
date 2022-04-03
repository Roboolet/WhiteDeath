using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowstorm : MonoBehaviour
{
    [SerializeField] float secondsUntilStorm;
    [Space]
    [SerializeField] float particleRate;
    [SerializeField] float particleLifetime, wind, entropyMultiplier;
    [SerializeField] ParticleSystem ps_white, ps_black;
    [SerializeField] ParticleSystemForceField ps_forcefield;

    bool started;
    PlayerThermometer player;

    private void Start()
    {
        player = FindObjectOfType<PlayerThermometer>();
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
        yield return new WaitForSeconds(secondsUntilStorm / 4);
        SetEffects(0.25f);
        yield return new WaitForSeconds(secondsUntilStorm / 4);
        SetEffects(0.5f);
        yield return new WaitForSeconds(secondsUntilStorm / 4);
        SetEffects(0.75f);
        yield return new WaitForSeconds(secondsUntilStorm/4);
        SetEffects(1);
        yield return new WaitForSeconds(secondsUntilStorm);
        SetEffects(1.5f);
    }

    void SetEffects(float mult)
    {
        // gameplay effects
        player.entropyMultWeather = entropyMultiplier * mult;

        // visual effects
        var emissionW = ps_white.emission;
        var emissionB = ps_black.emission;

        emissionW.rateOverTimeMultiplier = particleRate * mult;
        emissionB.rateOverTimeMultiplier = particleRate * mult;

        var mainW = ps_white.main;
        var mainB = ps_black.main;

        if (mult < 1)
        {
            mainW.startLifetime = particleLifetime / mult;
            mainB.startLifetime = particleLifetime / mult;
        }
        else
        {
            mainW.startLifetime = particleLifetime * mult;
            mainB.startLifetime = particleLifetime * mult;
        }

        ps_forcefield.directionX = wind * mult;
    }
}
