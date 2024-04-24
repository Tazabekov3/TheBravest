using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance {get; private set;}
    public GameObject inventoryPanel;
    public GameObject gameOverPanel;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        SetUIPanelActive(inventoryPanel, false);
        SetUIPanelActive(gameOverPanel, false);
    }

    void Update() {
        
    }

    public void SetUIPanelActive(GameObject uiPanel, bool isActive) {
        uiPanel.SetActive(isActive);
    }

    public void ToggleUIPanelActive(GameObject uiPanel) {
        uiPanel.SetActive(!uiPanel.activeInHierarchy);
    }

    public void GameOverScreen() {
        SetUIPanelActive(gameOverPanel, true);
        SetUIPanelActive(inventoryPanel, false);
    }
}
