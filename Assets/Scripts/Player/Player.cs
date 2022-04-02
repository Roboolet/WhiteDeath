using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Items;

public class Player : MonoBehaviour
{
    public static Player current;

    [Header("Movement Settings")]
    [SerializeField] float speed;
    [SerializeField] float speedMax;
    [SerializeField] float friction;

    [Header("Interactable Settings")]
    [SerializeField] float checkRadius;
    [SerializeField] ContactFilter2D contactFilter;
    Interactable selected;

    Item heldItem;

    #region Movement
    Vector2 move, moveRaw;
    float moveRawMagnitude;

    public void Input_Move(CallbackContext input) { moveRaw = input.ReadValue<Vector2>(); }

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
        if (selected != null && Vector2.Distance(transform.position, selected.transform.position) > selected.deselectDistance) SelectInteractable(null);

    }

    private void Awake()
    {
        current = this;
    }

    public void Input_Interact(CallbackContext input)
    {
        if (selected != null)
        {
            if (heldItem == null) selected.Use();
            else
            {
                if (selected.TryInsert(heldItem) && selected.takeInsertedItem)
                {
                    heldItem = null;
                }
            }
        }
    }

    public void Input_Drop(CallbackContext input)
    {
        if (heldItem != null)
        {
            ItemDropper.current.DropItem(heldItem, transform.position);
            heldItem = null;
        }
        
    }

    public bool TryPickUp(Item item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            return true;
        }
        else return false;
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

        Collider2D[] colls = new Collider2D[6];
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
