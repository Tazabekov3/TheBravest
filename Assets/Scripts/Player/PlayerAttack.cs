using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public int damage = 2;
    public float attackCooldown = 0.3f;
    private float attackTimer;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask enemyLayer;

    void Start() {
        
    }

    void Update() {
        if (attackTimer <= 0) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                attackTimer = attackCooldown;
                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesHit.Length; i++) {
                    enemiesHit[i].GetComponent<Enemy>().TakeDamage(damage);
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
