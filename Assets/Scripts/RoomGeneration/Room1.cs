using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour {
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    public Vector2Int RoomIndex { get; set; }
    
    public void OpenDoor (Vector2Int direction) {
        if (direction == Vector2Int.up) {
            topDoor.SetActive(true);
            topWall.SetActive(false);
        }

        if (direction == Vector2Int.down) {
            bottomDoor.SetActive(true);
            bottomWall.SetActive(false);
        }
        
        if (direction == Vector2Int.left) {
            leftDoor.SetActive(true);
            leftWall.SetActive(false);
        }
        
        if (direction == Vector2Int.right) {
            rightDoor.SetActive(true);
            rightWall.SetActive(false);
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }
}
