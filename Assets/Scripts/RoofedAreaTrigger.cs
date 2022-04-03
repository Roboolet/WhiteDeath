using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoofedAreaTrigger : MonoBehaviour
{
    [SerializeField] float colorLerp;
    [SerializeField] Image overlayL, overlayR;
    bool playerIsInside = true;
    Camera mainCam;

    [SerializeField] UnityEvent OnEntry, OnExit;

    private void Awake()
    {
        mainCam = Camera.main;
        OnEntry.Invoke();
    }

    private void LateUpdate()
    {
        if (playerIsInside)
        {
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, Color.black, colorLerp);
            overlayL.color = Color.Lerp(overlayL.color, Color.white, colorLerp);
            overlayR.color = Color.Lerp(overlayR.color, Color.white, colorLerp);
        }
        else
        {
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, Color.white, colorLerp);
            overlayL.color = Color.Lerp(overlayL.color, Color.black, colorLerp);
            overlayR.color = Color.Lerp(overlayR.color, Color.black, colorLerp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInside = true;
            OnEntry.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInside = false;
            OnExit.Invoke();
        }
    }
}
