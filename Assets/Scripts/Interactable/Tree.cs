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


    protected override void OnInsert(Item item)
    {
        base.OnInsert(item);
        chopsLeft -= (item as Axe).hitsPerSwing;

        if (chopsLeft <= 0)
        {
            spriteRenderer.sprite = choppedSprite;
            stopAllInteractions = true;
            GetComponent<Animator>().SetTrigger("Chopped");
        }
    }

    public void Anim_Fall()
    {
        for (int i = 0; i < numDrops; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-3f, -1), transform.position.y + Random.Range(-0.4f, 0.4f));
            ItemDropper.current.DropItem(possibleDrops[Random.Range(0, possibleDrops.Length)], pos);
        }
    }
}
