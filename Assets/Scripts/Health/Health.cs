using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;
    protected Animator animator;

    protected virtual void Start() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() {
        if (currentHealth <= 0) {
            if (animator != null) animator.SetTrigger("death");
            Destroy(gameObject, 2f);
        }
    }

    public virtual void TakeDamage(int damage) {
        if (animator != null) animator.SetTrigger("hurt");
        currentHealth -= damage;
    }

    public void RestoreHealth(int health) {
        currentHealth += health;
    }
}
