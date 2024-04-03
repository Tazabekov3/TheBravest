using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    // public void AddItem(Item item) {
    //     inventory.Add(item);
    // }

    // public void RemoveItem(Item item) {
    //     inventory.Remove(item);
    // }

    public void AddItem(ItemData itemData) {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item)) {
            item.AddToStack();
        } else {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void RemoveItem(ItemData itemData) {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item)) {
            item.RemoveFromStack();
            if (item.stackSize == 0) {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);

            }
        }
    }
}
