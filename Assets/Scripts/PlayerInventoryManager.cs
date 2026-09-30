using System;
using Items;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Main;
    private ItemContainer _itemContainer;

    private void Awake()
    {
        Main = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(ItemStack itemStack)
    {
        _itemContainer.AddItem(itemStack);
    }

    public void RemoveFromInventory(ItemStack itemStack)
    {
        _itemContainer.RemoveItemStack(itemStack);
    }
    
    public ItemContainer GetItemAt(int index)
    {
        return _itemContainer;
    }

    public void SetItemAt(int index, ItemContainer itemContainer)
    {
        return;
    }
}
