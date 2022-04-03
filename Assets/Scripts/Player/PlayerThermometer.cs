using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThermometer : Thermometer
{
    [Header("Effect Settings")]
    [SerializeField, Range(0, 1)] float effectThreshold;
    [SerializeField] float shakeRate, zoomRate, overlayRate;

    [Header("Effect References")]
    [SerializeField] CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin vcam_noise;
    [SerializeField] Image overlayL, overlayR;

    float vcam_defaultSize;

    private void Awake()
    {
        vcam_defaultSize = vcam.m_Lens.OrthographicSize;
        vcam_noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        SetEffects(0);
    }

    private void LateUpdate()
    {
        float thr = Mathf.Max(0, NormalizedColdness - effectThreshold);

        if (thr != 0)
        {
            SetEffects(thr);
        }
    }

    void SetEffects(float mult)
    {
        vcam.m_Lens.OrthographicSize = vcam_defaultSize * (1 - mult * zoomRate);
        vcam_noise.m_FrequencyGain = mult * shakeRate;
        vcam_noise.m_AmplitudeGain = mult * shakeRate;

        overlayL.fillAmount = mult * overlayRate;
        overlayR.fillAmount = mult * overlayRate;
    }

    protected override void OnExceedMinTemp()
    {
        base.OnExceedMinTemp();
        //game over
    }
}
