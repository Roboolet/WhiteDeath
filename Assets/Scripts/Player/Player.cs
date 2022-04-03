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

    [Header("References")]
    [SerializeField] Animator anim;
    [SerializeField] PlayerThermometer thermometer;

    Item heldItem;
    AnimationDirection animDir;

    Vector2 move, moveRaw;
    float moveRawMagnitude;

    public void Input_Move(CallbackContext input) { moveRaw = input.ReadValue<Vector2>(); }

    private void FixedUpdate()
    {
        float mult = Mathf.Clamp(1.2f - thermometer.NormalizedColdness, 0.25f, 1);
        transform.Translate(move * mult);
        anim.SetFloat("Speed", mult);
    }

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

        // Makes the animator choose what direction to face
        // Anti-clockwise
        Vector2 norm = moveRaw.normalized;
        if (norm.x <= -0.5f) animDir = AnimationDirection.Left;
        else if (norm.x >= 0.5f) animDir = AnimationDirection.Right;
        else if (norm.y <= -0.5f) animDir = AnimationDirection.Down;
        else if (norm.y >= 0.5f) animDir = AnimationDirection.Up;
        
        anim.SetInteger("Dir", (int)animDir);
        anim.SetFloat("Vel", move.sqrMagnitude * 100);

    }

    private void Awake()
    {
        current = this;
        
    }

    public void Input_Interact(CallbackContext input)
    {
        if (input.performed && selected != null)
        {
            if (heldItem == null) selected.Use();
            else
            {                
                if (selected.TryInsert(heldItem) && selected.takeInsertedItem)
                {
                    heldItem = null;
                }
                else if (heldItem.type == ItemType.Axe) anim.SetTrigger("Chop");
            }
        }
    }

    public void Input_Drop(CallbackContext input) => DropItem();

    void DropItem()
    {
        if (heldItem != null)
        {
            ItemDropper.current.DropItem(heldItem, transform.position);
            heldItem = null;
            SFXLib.current.Play("grab");
        }
    }

    public void PickUp(Item item)
    {
        DropItem();
        heldItem = item;
        SFXLib.current.Play("grab");
    }



    void SelectInteractable(Interactable inter)
    {
        if (selected != null) selected.Unprime();

        selected = inter;

        if (selected != null)
        {
            selected.Prime(heldItem != null);
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
                        Interactable inter = c.GetComponent<Interactable>();
                        if (!inter.stopAllInteractions)
                        {
                            shortestDist = dist;
                            output = inter;
                        }
                    }
                }
            }            
        }

        if (output != null) return true;
        else return false;
    }

    public void Anim_Step()
    {
        if (RoofedAreaTrigger.isInRoofedArea)
        {
            SFXLib.current.Play("tree_hit");
        }
        else SFXLib.current.Play("step");
    }
}

public enum AnimationDirection
{
    Up,
    Left,
    Down,
    Right
}
