using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory {
    public static Action<List<InventoryItem>> OnInventoryChanged;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public int size;
    public Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    public Inventory(int size) {
        inventory = new List<InventoryItem>(size);
        this.size = size;
    }

    public void AddItem(ItemData itemData) {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item)) {
            if (itemData.isStackable) {
                item.AddToStack();
            } else {
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                // itemDictionary.Add(itemData, newItem);
            }
            OnInventoryChanged?.Invoke(inventory);
        } else {
            if (!isFull()) {
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                itemDictionary.Add(itemData, newItem);
                OnInventoryChanged?.Invoke(inventory);
            }
        }
    }

    public void RemoveItem(ItemData itemData) {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item)) {
            item.RemoveFromStack();
            if (item.stackSize == 0) {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChanged?.Invoke(inventory);
        }
    }

    public bool isFull() {
        return inventory.Capacity == inventory.Count;
    }
}
