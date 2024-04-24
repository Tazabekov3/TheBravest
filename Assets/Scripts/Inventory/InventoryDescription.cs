using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDescription : MonoBehaviour {
    public Image icon;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI descriptionLabel;

    void OnEnable() {
        InventoryManager.InventoryMenuClosed += ClearItemDesciption;
        InventorySlot.SlotActivated += DrawItemDescription;
    }

    void OnDisable() {
        InventoryManager.InventoryMenuClosed -= ClearItemDesciption;
        InventorySlot.SlotActivated -= DrawItemDescription;
    }

    void Start() {
        icon.sprite = null;
        icon.preserveAspect = true;
    }

    void Update() {
        
    }

    public void DrawItemDescription(ItemData itemData, GameObject _gameObject) {
        if (itemData != null) {
            icon.sprite = itemData.itemSprite;
            nameLabel.text = itemData.itemName;
            descriptionLabel.text = itemData.itemDescription;
        }
    }

    public void ClearItemDesciption() {
        icon.sprite = null;
        nameLabel.text = "";
        descriptionLabel.text = "";
        Debug.Log("Cleared");
    }
}
