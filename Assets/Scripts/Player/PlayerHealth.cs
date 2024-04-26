using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health {
    private PlayerController playerController;

    protected override void Start() {
        base.Start();
        playerController = GetComponent<PlayerController>();
    }

    protected override void Update() {
        if (currentHealth <= 0) {
            Death();
        }
    }

    public override void TakeDamage(int damage) {
        if (!playerController.invulnerable) {
            currentHealth -= damage;
            StartCoroutine(playerController.IFrames());
            if (animator != null) animator.SetTrigger("hurt");
        }
    }

    void Death() {
        if (animator != null) animator.SetTrigger("death");
        Destroy(gameObject, 2f);
        UIManager.instance.GameOverScreen();
    }
}
