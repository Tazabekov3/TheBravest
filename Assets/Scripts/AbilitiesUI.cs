using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour {
    public static AbilitiesUI instance {get; private set;}
    public TextMeshProUGUI dashCDDisplay;
    public TextMeshProUGUI healthDisplay;
    private PlayerController playerController;
    private Health playerHealth;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<Health>();
    }

    void Update() {
        healthDisplay.text = playerHealth.currentHealth.ToString();
    }
}
