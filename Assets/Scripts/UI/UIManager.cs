using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance {get; private set;}
    public static event Action InventoryMenuOpened;
    public GameObject inventoryPanel;
    public GameObject gameOverPanel;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        if (inventoryPanel == null) {
            GameObject.Find("InventoryPanel");
        }
        if (gameOverPanel == null) {
            GameObject.Find("GameOverPanel");
        }
    }

    void Update() {
        if (inventoryPanel == null) {
            GameObject.Find("InventoryPanel");
        }
        if (gameOverPanel == null) {
            GameObject.Find("GameOverPanel");
        }

        if (Input.GetButtonDown("Inventory")) {
            Debug.Log(inventoryPanel.activeInHierarchy);
            ToggleUIPanelActive(inventoryPanel);
            if (inventoryPanel.activeInHierarchy) {
                InventoryMenuOpened?.Invoke();                
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SetUIPanelActive(inventoryPanel, false);
        SetUIPanelActive(gameOverPanel, false);
    }

    private void SetUIPanelActive(GameObject uiPanel, bool isActive) {
        uiPanel.SetActive(isActive);
        SetBlockRaycast(uiPanel, isActive);
    }

    private void ToggleUIPanelActive(GameObject uiPanel) {
        SetUIPanelActive(uiPanel, !uiPanel.activeInHierarchy);
    }

    public void GameOverScreen() {
        SetUIPanelActive(gameOverPanel, true);
        SetUIPanelActive(inventoryPanel, false);
    }

    public void SetBlockRaycast(GameObject uiPanel, bool isBlocking) {
        uiPanel.GetComponent<CanvasGroup>().blocksRaycasts = isBlocking;
    }
}
