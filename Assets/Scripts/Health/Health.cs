using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    protected virtual void Update() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
    }

    public void RestoreHealth(int health) {
        currentHealth += health;
    }
}
