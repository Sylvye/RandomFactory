using System;
using UnityEngine;
using Object = System.Object;

namespace Items
{
    [Serializable]
    public abstract class Item
    {
        protected readonly Sprite icon;
        protected readonly ItemType itemType;
    
        public Item(ItemType itemType, Sprite icon)  
        {
            this.itemType = itemType;
            this.icon = icon;
        }
    
        public ItemType GetItemType()
        {
            return itemType;
        }
        
        public Sprite GetIcon()
        {
            return icon;
        }

        public override bool Equals(object obj)
        {
            if (obj is Item item)
            {
                return icon.Equals(item.icon) &&
                       itemType.Equals(item.itemType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return icon.GetHashCode() ^ itemType.GetHashCode();
        }
    }
}
