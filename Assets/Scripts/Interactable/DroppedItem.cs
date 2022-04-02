using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DroppedItem: Interactable
{
    [Space]
    public Item item;

    protected override void OnUse()
    {
        base.OnUse();
        if (Player.current.TryPickUp(item))
        {
            Destroy(gameObject);
        }
    }
}
