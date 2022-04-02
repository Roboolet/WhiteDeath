using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Items", menuName = "Items/Burnable", order = 1)]
    public class Burnable : Item
    {
        [Header("Item Settings")]
        public float burnTime;
    }
}
