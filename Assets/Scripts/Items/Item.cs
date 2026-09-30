public abstract class Item
{
    protected ItemType itemType;
    
    public Item(ItemType iType) 
    {
        itemType = iType;
    }
    
    public ItemType GetItemType()
    {
        return itemType;
    }
    
    public void SetItemType(ItemType iType)
    {
        itemType = iType;
    }
}
