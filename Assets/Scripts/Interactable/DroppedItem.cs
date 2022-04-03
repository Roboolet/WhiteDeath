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
        Player.current.PickUp(item);
        Destroy(gameObject);
    }

    protected override void OnInsert(Item itemInput) => OnUse();
}
