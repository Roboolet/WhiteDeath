using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeatSource))]
public class Fireplace : Interactable
{
    HeatSource heatSource;

    protected override void Awake()
    {
        base.Awake();
        heatSource = GetComponent<HeatSource>();

    }

    protected override void OnInsert(Item item)
    {
        base.OnInsert(item);
    }
}
