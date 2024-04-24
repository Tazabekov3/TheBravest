using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Pathfinding.Util;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance {get; private set;}
    public static event Action InventoryMenuClosed;
    public GameObject inventoryMenu;
    private bool menuActive = false;
    private Inventory inventoryList;
    public Transform slotsChild;
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private GameObject currentActiveSlot;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        Inventory.OnInventoryChanged += DrawInventoryUI;
        InventorySlot.SlotActivated += SlotSetActive;
    }

    void OnDisable() {
        Inventory.OnInventoryChanged -= DrawInventoryUI;
        InventorySlot.SlotActivated -= SlotSetActive;
    }

    void Start() {
        int inventorySize = 16;
        inventoryList = new Inventory(inventorySize);
        inventorySlots = new List<InventorySlot>(inventorySize);
        slotsChild = inventoryMenu.transform.Find("InventorySlots");
        DrawInventoryUI(inventoryList.inventory);
        inventoryMenu.SetActive(menuActive);
    }

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
            inventoryMenu.SetActive(!inventoryMenu.activeInHierarchy);
            // menuActive = !menuActive;
            if (inventoryMenu.activeInHierarchy) {
                Debug.Log(InventoryMenuClosed == null);
                InventoryMenuClosed?.Invoke();
                if (currentActiveSlot != null) currentActiveSlot.GetComponent<InventorySlot>().SetSlotActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            foreach (InventoryItem stuff in inventoryList.inventory) {
                Debug.Log(stuff.itemData.itemName);
                Debug.Log(stuff.stackSize);
            }
        }
    }

    public void AddItem(ItemData itemData) {
        inventoryList.AddItem(itemData);
    }

    public void RemoveItem(ItemData itemData) {
        inventoryList.RemoveItem(itemData);
    }

    void ClearInventoryUI() {
        foreach (Transform childTransform in slotsChild) Destroy(childTransform.gameObject);
        inventorySlots = new List<InventorySlot>();
    }

    void DrawInventoryUI(List<InventoryItem> inventory) {
        ClearInventoryUI();

        for (int i = 0; i < inventory.Capacity; i++) {
            GameObject newSlot = Instantiate(slotPrefab);
            newSlot.transform.SetParent(slotsChild, false);
            newSlot.name = $"InventorySlot-{i + 1}";
            
            InventorySlot newSlotScript = newSlot.GetComponent<InventorySlot>();
            newSlotScript.SetSlotEnabled(false);

            inventorySlots.Add(newSlotScript);
            // inventorySlots[i].DrawSlot(inventory[i]);
        }

        for (int i = 0; i < inventory.Count; i++) {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void SlotSetActive(ItemData itemData, GameObject slot) {
        if (currentActiveSlot != null) currentActiveSlot.GetComponent<InventorySlot>().SetSlotActive(false);
        slot.GetComponent<InventorySlot>().SetSlotActive(true);
        currentActiveSlot = slot;
    }
}
