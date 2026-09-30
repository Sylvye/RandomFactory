using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public abstract class Item
    {
        [SerializeField] protected ItemType itemType;
    
        public Item(ItemType iType) 
        {
            itemType = iType;
        }
    
        public ItemType GetItemType()
        {
            return itemType;
        }
    }
}
