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
        InventorySlot.SlotActivated += DrawItemDescription;
    }

    void OnDisable() {
        InventorySlot.SlotActivated -= DrawItemDescription;
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public void DrawItemDescription(ItemData itemData, GameObject _gameObject) {
        if (itemData != null) {
            icon.sprite = itemData.itemSprite;
            icon.preserveAspect = true;
            nameLabel.text = itemData.itemName;
            descriptionLabel.text = itemData.itemDescription;
        }
    }
}
