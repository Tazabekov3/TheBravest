using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI1 : MonoBehaviour {
    public float roamDistance = 3f;
    public float sightDistance = 6f;
    public float attackRange = 1f;
    public Transform attackPoint;
    public float returnDelay = 3f;
    private AIDestinationSetter1 destinationSetter;
    private Transform player;
    private float returnTimer;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 destinationPosition;
    private enum State {Roaming, Chasing, Lost, OnSpawn};
    private State currentState;

    void Start() {
        destinationSetter = GetComponent<AIDestinationSetter1>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = transform.position;
        returnTimer = returnDelay;
        currentState = State.Roaming;
        destinationPosition = GetRoamingPosition();
    }

    void Update() {
        // Debug.Log(currentState);

        switch (currentState) {
            case State.Roaming:
                MoveToPosition(destinationPosition);

                if (Vector3.Distance(transform.position, destinationPosition) <= 1f) {
                    destinationPosition = GetRoamingPosition();
                    MoveToPosition(destinationPosition);
                }
                break;
            case State.Chasing:
                MoveToPosition(player.position);

                if (Vector3.Distance(transform.position, player.position) <= attackRange) {
                    Attack();
                }
                break;
            case State.Lost:
                returnTimer -= Time.deltaTime;
                if (returnTimer <= 0f) {
                    MoveToSpawn();
                    returnTimer = returnDelay;
                }
                break;
            case State.OnSpawn:
                break;
        }

        if (Vector3.Distance(transform.position, player.position) <= sightDistance) {
            currentState = State.Chasing;
        } else if (Vector3.Distance(transform.position, spawnPosition) <= roamDistance && 
                   Vector3.Distance(transform.position, player.position) > sightDistance) {
            currentState = State.Roaming;
        } else if (Vector3.Distance(transform.position, player.position) > sightDistance && 
                   Vector3.Distance(transform.position, spawnPosition) > roamDistance) {
            currentState = State.Lost;
        }
    }

    void Attack() {
        Debug.Log("Attack the player");
    }

    void MoveToPosition(Vector3 position) {
        destinationSetter.target = position;
    }

    void MoveToSpawn() {
        destinationSetter.target = spawnPosition;
    }

    Vector3 GetRoamingPosition() {
        Vector3 randomDirection = spawnPosition + (Random.insideUnitSphere * roamDistance);
        // randomDirection += transform.position;
        randomDirection.z = transform.position.z;
        return randomDirection;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
        Gizmos.DrawWireSphere(spawnPosition, roamDistance);

        // Gizmos.DrawWireCube(spawnAnchor.position, new Vector3(0.1f, 0.1f, 0.1f));
        // Gizmos.DrawIcon(anchor.position, "Light Gizmo.tiff", true);
    }
}
