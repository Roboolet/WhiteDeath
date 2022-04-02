using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoofedAreaTrigger : MonoBehaviour
{
    [SerializeField] float colorLerp;
    [SerializeField] UnityEvent OnEntry, OnExit;
    bool playerIsInside;
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (playerIsInside)
        {
            Color c;
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, Color.black, colorLerp);
        }
        else
        {
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, Color.white, colorLerp);
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
