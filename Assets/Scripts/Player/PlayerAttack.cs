using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public int damage = 2;
    public float attackCooldown = 0.4f;
    private float attackTimer;
    private Animator animator;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask enemyLayer;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (attackTimer <= 0) {
            if (Input.GetButtonDown("Fire1")) {
                attackTimer = attackCooldown;
                animator.SetTrigger("attack");
                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesHit.Length; i++) {
                    enemiesHit[i].GetComponent<Health>().TakeDamage(damage);
                    Debug.Log("Enemy Hit");
                }
            }
        } else {
            attackTimer -= Time.deltaTime;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
