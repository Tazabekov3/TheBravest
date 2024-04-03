using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;

    public void Start() {
        AddSprite();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            InventoryManager.instance.AddItem(itemData);
            Debug.Log("Picked up");
            Destroy(gameObject);
        }
    }

    void AddSprite() {
        Transform spriteChild = transform.Find("Sprite");

        if (spriteChild == null) {
            GameObject spriteObject = new GameObject("Sprite");
            spriteObject.transform.parent = transform;
            spriteObject.transform.localPosition = Vector3.zero;

            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.itemSprite;
        } else {
            SpriteRenderer spriteRenderer = spriteChild.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null) spriteRenderer.sprite = itemData.itemSprite;
            else spriteChild.gameObject.AddComponent<SpriteRenderer>().sprite = itemData.itemSprite;
        }
    }
}
