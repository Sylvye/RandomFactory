using System.Collections.Generic;

public class ItemContainer
{
    protected int size;
    protected List<ItemType> itemTypes;
    protected List<Item> items;
    
    public ItemContainer() 
    {
        size = 0;
        items = new();
    }
    
    public ItemContainer(int size) 
    {
        this.size = size;
    }
    
    public ItemContainer(int size, List<Item> items) 
    {
        this.size = size;
        this.items = items;
    }
    
    public ItemContainer(List<Item> items) 
    {
        size = items.Count;
        this.items = items;
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
        items.Add(item);
    }
    
    public void RemoveItem(Item item) 
    {
        items.Remove(item);
    }
}
