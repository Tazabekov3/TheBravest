using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerDownHandler {
    public static event Action<ItemData, GameObject> SlotActivated;
    public Image icon;
    public TextMeshProUGUI stackLabel;
    public Image activeFrame;
    public ItemData itemData;

    void Start() {
        activeFrame.enabled = false;
        icon.preserveAspect = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (itemData != null) SlotActivated?.Invoke(itemData, this.gameObject);
        else return;
    }

    public void SetSlotEnabled(bool isEnabled) {
        icon.enabled = isEnabled;
        stackLabel.enabled = isEnabled;
    }

    public void SetSlotActive(bool isActive) {
        activeFrame.enabled = isActive;
    }

    public void DrawSlot(InventoryItem item) {
        if (item == null) {
            SetSlotEnabled(false);
            return;
        }

        SetSlotEnabled(true);

        itemData = item.itemData;
        icon.sprite = itemData.itemSprite;
        stackLabel.text = item.stackSize <= 1 ? null : item.stackSize.ToString();
    }
}
