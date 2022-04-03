using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable
{
    [Header("Tree settings")]
    [SerializeField] Sprite choppedSprite;
    [SerializeField] float chopsLeft;


    protected override void OnInsert(Item item)
    {
        base.OnInsert(item);
        chopsLeft -= (item as Axe).hitsPerSwing;

        if (chopsLeft <= 0)
        {
            spriteRenderer.sprite = choppedSprite;
            stopAllInteractions = true;
        }
    }

    public void Anim_Fall()
    {

    }
}
