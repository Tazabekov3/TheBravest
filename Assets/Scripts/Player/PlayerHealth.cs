using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {
    protected override void Update() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
            Death();
        }
    }

    void Death() {
        UIManager.instance.GameOverScreen();
    }
}
