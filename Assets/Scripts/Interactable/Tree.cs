using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable
{
    [Header("Tree settings")]
    [SerializeField] Sprite choppedSprite;
    [SerializeField] float chopsLeft;

    [Space]
    [SerializeField] Item[] possibleDrops;
    [SerializeField] int numDrops;
    [Space]
    [SerializeField] Animator anim;
    bool canFall = true;


    protected override void OnInsert(Item item)
    {
        base.OnInsert(item);
        chopsLeft -= (item as Axe).hitsPerSwing;
        SFXLib.current.Play("tree_hit");

        if (chopsLeft <= 0 && canFall)
        {
            spriteRenderer.sprite = choppedSprite;
            stopAllInteractions = true;
            anim.SetTrigger("Chopped");
            canFall = false;
        }
    }

    public void Anim_Fall()
    {
        for (int i = 0; i < numDrops; i++)
        {
            Vector2 pos = transform.position + Random.Range(-4f, -1) * transform.right + Random.Range(-0.4f, 0.4f) * transform.up;
            ItemDropper.current.DropItem(possibleDrops[Random.Range(0, possibleDrops.Length)], pos);
        }

        SFXLib.current.Play("tree_fall");
    }
}
