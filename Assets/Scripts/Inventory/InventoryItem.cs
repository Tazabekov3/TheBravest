using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem {
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData item) {
        itemData = item;
        AddToStack();
    }

    public void AddToStack() {
        stackSize++;
    }

    public void RemoveFromStack() {
        stackSize--;
    }

    // void AddSprite() {
    //     Transform spriteChild = transform.Find("Sprite");

    //     if (spriteChild == null) {
    //         GameObject spriteObject = new GameObject("Sprite");
    //         spriteObject.transform.parent = transform;
    //         spriteObject.transform.localPosition = Vector3.zero;

    //         SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
    //         spriteRenderer.sprite = itemData.itemSprite;
    //     } else {
    //         SpriteRenderer spriteRenderer = spriteChild.GetComponent<SpriteRenderer>();

    //         if (spriteRenderer != null) spriteRenderer.sprite = itemData.itemSprite;
    //         else spriteChild.gameObject.AddComponent<SpriteRenderer>().sprite = itemData.itemSprite;
    //     }
    // }

    // void OnCollisionEnter2D(Collision2D collision) {
    //     if (collision.gameObject.CompareTag("Player")) {
    //         InventoryManager.instance.AddItem(itemData);
    //         Destroy(gameObject);
    //         Debug.Log(itemData);
    //     }
    // }
}
