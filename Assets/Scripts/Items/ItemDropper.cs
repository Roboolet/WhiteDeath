using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemDropper : MonoBehaviour
    {
        public static ItemDropper current;

        [SerializeField] ItemDropperData[] _lib;
        Dictionary<Item, GameObject> lib = new Dictionary<Item, GameObject>();

        private void Awake()
        {
            current = this;
            foreach (ItemDropperData i in _lib)
            {
                lib.Add(i.item, i.prefab);
            }
        }

        public void DropItem(Item item, Vector2 pos)
        {
            Instantiate(lib[item], pos, Quaternion.identity);
        }
    }

    [System.Serializable]
    public struct ItemDropperData
    {
        public Item item;
        public GameObject prefab;
    }
}
