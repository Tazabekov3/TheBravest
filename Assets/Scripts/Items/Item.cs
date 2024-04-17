using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;
    private Transform spriteRenderer;
    private BoxCollider2D itemCollider;

    void Start() {
        itemCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = AddSprite();
        spriteRenderer.tag = transform.tag;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            InventoryManager.instance.AddItem(itemData);
            Debug.Log("Picked up");
            Destroy(gameObject);
        }
    }

    Transform AddSprite() {
        Transform spriteChild = transform.Find("Sprite");

        if (spriteChild == null) {
            GameObject spriteObject = new GameObject("Sprite");
            spriteChild = spriteObject.transform;
            spriteObject.transform.parent = transform;
            spriteObject.transform.localPosition = Vector3.zero;

            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.itemSprite;

            Bounds spriteBounds = spriteRenderer.bounds;
            itemCollider.size = spriteBounds.size;
            itemCollider.offset = spriteBounds.center - transform.position;
        } else {
            SpriteRenderer spriteRenderer = spriteChild.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null) spriteRenderer.sprite = itemData.itemSprite;
            else spriteChild.gameObject.AddComponent<SpriteRenderer>().sprite = itemData.itemSprite;
        }

        return spriteChild;
    }
}
