using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField] private bool isStatic;
    [SerializeField] private int layerOffset = 0;

    private SpriteRenderer sp;

    private void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();

        if (isStatic)
        {
            SetLayer();
        }
    }

    private void LateUpdate()
    {
        if (!isStatic && sp.isVisible) SetLayer();
    }

    void SetLayer()
    {
        sp.sortingOrder = Mathf.FloorToInt(sp.bounds.min.y * -1.0f) * 2 + layerOffset;
    }
}
