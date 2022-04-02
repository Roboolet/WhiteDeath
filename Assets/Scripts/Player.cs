using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float friction;
    //[Header("References")]

    Vector2 move, moveRaw;
    float moveRawMagnitude;

    public void Input_Move(CallbackContext input) { moveRaw = input.ReadValue<Vector2>(); }

    public void Input_Interact(CallbackContext input) { }

    private void Update()
    {
        float currentSpeedMax = speedMax * moveRawMagnitude;
        if (moveRaw != Vector2.zero)
            move = Vector2.ClampMagnitude(move + moveRaw * speed * Time.deltaTime, currentSpeedMax);
        else
            move = Vector2.Lerp(move, Vector2.zero, friction * Time.deltaTime);

        moveRawMagnitude = moveRaw.magnitude;
    }

    private void FixedUpdate()
    {
        transform.Translate(move);
    }
}
