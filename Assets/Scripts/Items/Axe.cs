using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Items", menuName = "Items/Axe", order = 1)]
    public class Axe : Item
    {
        [Header("Item Settings")]
        public int hitsPerSwing;
    }
}
