using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float friction;

    [Header("Interactable Settings")]
    [SerializeField] float checkRadius;
    [SerializeField] ContactFilter2D contactFilter;
    Interactable selected;

    #region Movement and input stuff
    Vector2 move, moveRaw;
    float moveRawMagnitude;

    public void Input_Move(CallbackContext input) { moveRaw = input.ReadValue<Vector2>(); }

    public void Input_Interact(CallbackContext input) { }

    public void Input_Drop(CallbackContext input) { }

    private void FixedUpdate()
    {
        transform.Translate(move);
    }
    #endregion

    private void Update()
    {
        // simple smooth movement stuff
        // requires a dynamic rigidbody on the player, or collisions won't work
        float currentSpeedMax = speedMax * moveRawMagnitude;
        if (moveRaw != Vector2.zero)
            move = Vector2.ClampMagnitude(move + moveRaw * speed * Time.deltaTime, currentSpeedMax);
        else
            move = Vector2.Lerp(move, Vector2.zero, friction * Time.deltaTime);

        moveRawMagnitude = moveRaw.magnitude;

        Interactable inter;
        if(GetClosestInteractable(out inter))
        {
            SelectInteractable(inter);
        }

        // deselect when too far away
        if (selected != null && Vector2.Distance(transform.position, selected.transform.position) > checkRadius) SelectInteractable(null);

    }

    void SelectInteractable(Interactable inter)
    {
        if (selected != null) selected.Unprime();

        selected = inter;

        if (selected != null)
        {
            selected.Prime();
        }

    }

    bool GetClosestInteractable(out Interactable output)
    {
        output = null;

        Collider2D[] colls = new Collider2D[4];
        if (Physics2D.OverlapCircle(transform.position, checkRadius, contactFilter: contactFilter, colls) >= 1)
        {
            float shortestDist = Mathf.Infinity;

            for (int i = 0; i < colls.Length; i++)
            {
                Collider2D c = colls[i];
                if (c != null)
                {
                    float dist = (transform.position - c.transform.position).sqrMagnitude;
                    if (dist < shortestDist)
                    {
                        shortestDist = dist;
                        output = c.GetComponent<Interactable>();
                    }
                }
            }            
        }

        if (output != null) return true;
        else return false;
    }
}
