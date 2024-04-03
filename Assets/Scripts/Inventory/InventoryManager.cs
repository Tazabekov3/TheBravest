using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance {get; private set;}
    public GameObject inventoryMenu;
    private bool menuActive = false;
    private Inventory inventoryList;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        inventoryList = new Inventory();
        Debug.Log(inventoryList.inventory);
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
}
