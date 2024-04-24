using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager1 : MonoBehaviour {
    [SerializeField] GameObject roomPrefab;
    [SerializeField] int minRooms = 10;
    [SerializeField] int maxRooms = 15;

    int roomWidth = 12;
    int roomHeight = 6;

    int gridSizeX = 11;
    int gridSizeY = 11;

    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();
    private int[,] roomGrid;
    private int roomCount;

    private bool generationCompleted;
    private GameObject dungeon;

    void Start() {
        roomGrid = new int[gridSizeX, gridSizeY];

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        dungeon = GetDungeonObject();
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    void Update() {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationCompleted) {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateNewRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateNewRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateNewRoom(new Vector2Int(gridX, gridY - 1));
            TryGenerateNewRoom(new Vector2Int(gridX, gridY + 1));
        } else if (!generationCompleted) {
            generationCompleted = true;
            AstarPath.active.Scan();
        }
    }

    private GameObject GetDungeonObject() {
        GameObject dungeon = GameObject.Find("Rooms");
        if (dungeon == null) {
            dungeon = new GameObject("Rooms");
        }
        return dungeon;
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex) {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;

        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

        initialRoom.name = $"Room-{roomCount}";
        initialRoom.transform.SetParent(dungeon.transform);
        initialRoom.GetComponent<Room1>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateNewRoom(Vector2Int roomIndex) {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0) return false;

        if (roomCount >= maxRooms) return false;

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero) return false;

        if (CountAdjacentRooms(roomIndex) > 1) return false;

        if (roomGrid[x, y] != 0) return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;
        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

        newRoom.name = $"Room-{roomCount}";
        newRoom.transform.SetParent(dungeon.transform);
        newRoom.GetComponent<Room1>().RoomIndex = roomIndex;
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);

        return true;
    }

    private void OpenDoors(GameObject room, int x, int y) {
        Room1 newRoomScript = room.GetComponent<Room1>();

        Room1 leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room1 rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room1 topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room1 bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (x > 0 && roomGrid[x - 1, y] != 0) {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0) {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    private Room1 GetRoomScriptAt(Vector2Int roomIndex) {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room1>().RoomIndex == roomIndex);
        if (roomObject != null) return roomObject.GetComponent<Room1>();
        else return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex) {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex) {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos() {
        Color gizmoColor = new Color(1, 0, 0, 0.5f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
