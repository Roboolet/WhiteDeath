using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Item : ScriptableObject
    {
        public ItemType type;
    }

    public enum ItemType
    {
        Undefined,
        Burnable,
        Axe
    }
}