using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool primed { get; private set; }

    protected bool stopAllInserts;
    [SerializeField] protected ItemType[] allowedInserts;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite_primed, sprite_unprimed;

    protected virtual void Awake()
    {
        // layer 6 is "Interactable"
        gameObject.layer = 6;
    }

    public void Prime()
    {
        if (!primed)
        {
            primed = true;
            OnPrime();
        }
    }

    /// <summary>
    /// Called when this object is in usage range
    /// </summary>
    protected virtual void OnPrime()
    {
        spriteRenderer.sprite = sprite_primed;
    }

    public void Unprime()
    {
        if (primed)
        {
            primed = false;
            OnUnprime();
        }
    }

    /// <summary>
    /// Called when this object is out of usage range
    /// </summary>
    protected virtual void OnUnprime()
    {
        spriteRenderer.sprite = sprite_unprimed;
    }

    /// <summary>
    /// Called when a player presses the interact button near this object
    /// </summary>
    public virtual void OnUse() { }

    /// <summary>
    /// Try to insert the given item, returns false if it can't be inserted
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool TryInsert(Item item)
    {
        if (stopAllInserts) return false;

        for(int i = 0; i < allowedInserts.Length; i++)
        {
            if(allowedInserts[i] == item.type)
            {
                OnInsert(item);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Called when the player interacts with this object when they are holding something
    /// </summary>
    protected virtual void OnInsert(Item item) { }
}
