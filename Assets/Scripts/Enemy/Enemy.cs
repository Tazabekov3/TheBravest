using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health = 6;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (health <= 0) {
            Debug.Log("I am dead");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)  {
        this.health -= damage;
    }
}
