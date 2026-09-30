using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

[Serializable]
public class ItemContainer
{
    [SerializeField] protected int size;
    [SerializeField] protected List<ItemType> itemTypes;
    [SerializeReference] protected List<Item> items;
    
    public ItemContainer() 
    {
        size = 0;
        itemTypes = ItemTypeHelpers.AllItemTypes();
        items = new List<Item>();
    }
    
    public ItemContainer(int size) 
    {
        this.size = size;
        items = new List<Item>();
        ItemTypeHelpers.AllItemTypes();
    }
    
    public ItemContainer(int size, List<Item> items) 
    {
        this.size = size;
        this.items = items;
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }
    
    public ItemContainer(List<Item> items) 
    {
        size = items.Count;
        this.items = items;
        itemTypes = ItemTypeHelpers.AllItemTypes();
    }
    
    public int GetSize() 
    {
        return size;
    }
    
    public List<Item> GetItems() 
    {
        return items;
    }
    
    public void AddItem(Item item) 
    {
        if (itemTypes.Contains(item.GetItemType()))
            items.Add(item);
    }
    
    public void RemoveItem(Item item) 
    {
        items.Remove(item);
    }
}
