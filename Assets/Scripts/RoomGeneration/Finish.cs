using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("Player")) SceneManager.LoadScene("Level");
    }
}
