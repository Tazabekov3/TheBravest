using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnLoad : MonoBehaviour {
    public string id;

    void Awake() {
        id = name + transform.position.ToString();
    }

    void Start() {
        for (int i = 0; i < FindObjectsOfType<KeepOnLoad>().Length; i++) {
            if (FindObjectsOfType<KeepOnLoad>()[i] != this && FindObjectsOfType<KeepOnLoad>()[i].id == id) Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
