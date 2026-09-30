using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

[Serializable]
public class ItemContainer
{
    [SerializeField] protected int size;
    [SerializeField] protected int maxStackSize;
    [SerializeField] protected List<ItemType> itemTypes;
    [SerializeReference] protected List<ItemStack> items;
    
    public ItemContainer() 
    {
        size = 0;
        maxStackSize = 100;
        items = new List<ItemStack>();
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }

    public ItemContainer(int size) 
    {
        this.size = size;
        maxStackSize = 100;
        items = new List<ItemStack>();
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }

    public ItemContainer(int size, List<ItemStack> items) 
    {
        this.size = size;
        maxStackSize = 100;
        this.items = items;
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }
    
    public ItemContainer(int size, List<ItemStack> items, List<ItemType> itemTypes) 
    {
        this.size = size;
        maxStackSize = 100;
        this.items = items;
        this.itemTypes = itemTypes;
    }
    
    public ItemContainer(int size, List<ItemStack> items, int maxStackSize) 
    {
        this.size = size;
        this.maxStackSize = maxStackSize;
        this.items = items;
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }
    
    public ItemContainer(int size, List<ItemStack> items, List<ItemType> itemTypes, int maxStackSize) 
    {
        this.size = size;
        this.maxStackSize = maxStackSize;
        this.items = items;
        this.itemTypes = itemTypes;
    }

    public int GetSize() 
    {
        return size;
    }
    
    public List<ItemStack> GetItems() 
    {
        return items;
    }
    
    public void AddItem(ItemStack itemStack) 
    {
        if (AllowsType(itemStack.GetItemType()))
        {
            // if contains same item type, add amount to existing itemstack
            if (ContainsItem(itemStack.GetItem(), out ItemStack found) && found.amount < maxStackSize)
            {
                if (found.amount + itemStack.amount <= maxStackSize)
                {
                    found.amount += itemStack.amount;
                }
                else
                {
                    // revisit this later
                    itemStack.amount -= maxStackSize - found.amount;
                    found.amount = maxStackSize;
                }
            }
            else if (GetItemCount() < size) // else add new itemstack
            {
                items.Add(itemStack);
            }
        }
    }
    
    public bool RemoveItemStack(ItemStack item) 
    {
        return items.Remove(item);
    }

    public ItemStack GetItemStackAt(int index)
    {
        return items[index];
    }
    
    public void SetItemStackAt(int index, ItemStack item) 
    {
        items[index] = item;
    }

    public int GetIndexOf(ItemStack itemStack)
    {
        return items.IndexOf(itemStack);
    }
    
    public int GetIndexOf(Item item)
    {
        return items.FindIndex(itemStack => itemStack.GetItem().Equals(item));
    }
    
    public bool ContainsItemStack(ItemStack itemStack) 
    {
        return items.Contains(itemStack);
    }

    public bool ContainsItem(Item item)
    {
        return items.Exists(itemStack => itemStack.GetItem().Equals(item));
    }
    
    public bool ContainsItem(Item item, out ItemStack foundStack)
    {
        foundStack = items.Find(itemStack => itemStack.GetItem().Equals(item));
        return foundStack != null;
    }
    
    public bool ContainsType(ItemType itemType) 
    {
        return items.Exists(item => item.GetItemType().Equals(itemType));
    }
    
    public bool AllowsType(ItemType itemType) 
    {
        return itemTypes.Contains(itemType);
    }
    
    public int GetItemCount() 
    {
        return items.Count;
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
    
    public void Clear() 
    {
        items.Clear();
    }
}
