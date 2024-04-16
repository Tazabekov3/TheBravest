using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    // public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI stackLabel;

    void Start() {
        
    }

    void Update() {
        
    }

    public void SetSlot(bool isActive) {
        icon.enabled = isActive;
        // nameLabel.enabled = isActive;
        stackLabel.enabled = isActive;
    }

    public void DrawSlot(InventoryItem item) {
        if (item == null) {
            SetSlot(false);
            return;
        }

        SetSlot(true);

        icon.sprite = item.itemData.itemSprite;
        // nameLabel.text = item.itemData.itemName;
        stackLabel.text = item.stackSize.ToString();
    }
}
