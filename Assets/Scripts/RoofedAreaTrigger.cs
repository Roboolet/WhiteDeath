using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoofedAreaTrigger : MonoBehaviour
{
    public static bool isInRoofedArea;

    [SerializeField] float colorLerp;
    [SerializeField] Image overlayL, overlayR;
    Camera mainCam;

    [SerializeField] UnityEvent OnEntry, OnExit;

    private void Awake()
    {
        mainCam = Camera.main;
        Enter();
    }

    private void LateUpdate()
    {
        if (isInRoofedArea)
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

    void Enter()
    {
        OnEntry.Invoke();
        isInRoofedArea = true;
    }

    void Exit()
    {
        OnExit.Invoke();
        isInRoofedArea = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Exit();
        }
    }
}
