using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Pathfinding.Util;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance {get; private set;}
    public GameObject inventoryMenu;
    private bool menuActive = false;
    private Inventory inventoryList;
    public Transform slotsChild;
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

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
    }

    void OnDisable() {
        Inventory.OnInventoryChanged -= DrawInventoryUI;
    }

    void Start() {
        inventoryList = new Inventory();
        Debug.Log(inventoryList.inventory);
        inventorySlots = new List<InventorySlot>();
        slotsChild = inventoryMenu.transform.Find("InventorySlots");
    }

    void Update() {
        if (Input.GetButtonDown("Inventory")) {
            inventoryMenu.SetActive(!menuActive);
            menuActive = !menuActive;
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            foreach (InventoryItem stuff in inventoryList.inventory) {
                Debug.Log(stuff.itemData.itemName);
                Debug.Log(stuff.stackSize);
            }
        }

        // if (Input.GetButtonDown("Inventory") && !menuActive) {
        //     inventoryMenu.SetActive(true);
        //     menuActive = true;
        //     Debug.Log("I pressed");
        // } else if (Input.GetButtonDown("Inventory") && menuActive) {
        //     inventoryMenu.SetActive(false);
        //     menuActive = false;
        //     Debug.Log("I pressed");
        // }
    }

    public void AddItem(ItemData itemData) {
        inventoryList.AddItem(itemData);
    }

    public void RemoveItem(ItemData itemData) {
        inventoryList.RemoveItem(itemData);
    }

    Transform FindSlotsChild() {
        Transform slots = inventoryMenu.transform.Find("InventorySlots");
        Debug.Log(slots);
        if (slots != null) return slots;
        return null;
    }

    void ClearInventoryUI() {
        foreach (Transform childTransform in slotsChild) Destroy(childTransform.gameObject);
        inventorySlots = new List<InventorySlot>();
    }

    void DrawInventoryUI(List<InventoryItem> inventory) {
        ClearInventoryUI();

        for (int i = 0; i < inventory.Count; i++) {
            GameObject newSlot = Instantiate(slotPrefab, slotsChild);
            newSlot.name = $"InventorySlot-{i + 1}";
            
            InventorySlot newSlotScript = newSlot.GetComponent<InventorySlot>();
            newSlotScript.SetSlot(false);

            inventorySlots.Add(newSlotScript);
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    public void InventoryChanged(List<InventoryItem> inventory) {
        Debug.Log("Inventory has changed");
    }
}
