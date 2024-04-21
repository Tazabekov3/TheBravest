using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Events : MonoBehaviour {
    public static Events instance {get; private set;}
    public static Action<ItemData> SlotActivated;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
