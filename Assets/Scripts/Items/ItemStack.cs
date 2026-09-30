using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ItemStack
    {
        private Item itemReference;
        public int amount;

        public ItemStack(Item item)
        {
            itemReference = item;
            amount = 1;
        }
        
        public ItemStack(Item item, int amount) 
        {
            itemReference = item;
            this.amount = amount;
        }
        
        public Item GetItem() 
        {
            return itemReference;
        }

        public ItemType GetItemType()
        {
            return itemReference.GetItemType();
        }

        public Sprite GetIcon()
        {
            return itemReference.GetIcon();
        }
    }
}
