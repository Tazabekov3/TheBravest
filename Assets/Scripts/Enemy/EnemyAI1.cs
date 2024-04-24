using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI1 : MonoBehaviour {
    public float roamDistance = 3f;
    public float sightDistance = 6f;
    public float returnDelay = 3f;

    public int damage = 2;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    private AIDestinationSetter1 destinationSetter;
    private AIPath aiPath;
    private Transform player;
    private PlayerController playerController;
    private float attackTimer;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 destinationPosition;
    private enum State {Roaming, Chasing, Lost, Attacking};
    private State currentState;

    void Start() {
        destinationSetter = GetComponent<AIDestinationSetter1>();
        aiPath = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();

        spawnPosition = transform.position;
        attackTimer = attackCooldown;
        
        currentState = State.Roaming;
        destinationPosition = GetRoamingPosition();
    }

    void Update() {
        switch (currentState) {
            case State.Roaming:
                aiPath.canMove = true;
                MoveToPosition(destinationPosition);

                if (Vector3.Distance(transform.position, destinationPosition) <= 1f) {
                    destinationPosition = GetRoamingPosition();
                    MoveToPosition(destinationPosition);
                }
                break;
            case State.Chasing:
                aiPath.canMove = true;
                if (player != null) MoveToPosition(player.position);
                break;
            case State.Lost:
                aiPath.canMove = true;
                Invoke("MoveToSpawn", returnDelay);
                break;
            case State.Attacking:
                aiPath.canMove = false;
                if (attackTimer >= attackCooldown) {
                    Attack();
                    attackTimer = 0f;
                }
                attackTimer += Time.deltaTime;
                break;
        }

        if (player != null) {
            if (Vector3.Distance(transform.position, player.position) <= sightDistance &&
                Vector3.Distance(transform.position, player.position) > attackRange) {
                currentState = State.Chasing;
            } else if (Vector3.Distance(transform.position, spawnPosition) <= roamDistance && 
                    Vector3.Distance(transform.position, player.position) > sightDistance) {
                currentState = State.Roaming;
            } else if (Vector3.Distance(transform.position, player.position) > sightDistance && 
                    Vector3.Distance(transform.position, spawnPosition) > roamDistance) {
                currentState = State.Lost;
            } else if (Vector3.Distance(transform.position, player.position) <= attackRange) {
                currentState = State.Attacking;
            }
        } 
    }

    void Attack() {
        if (player != null && !playerController.invulnerable) {
            player.GetComponent<Health>().TakeDamage(damage);
        }
        Debug.Log("Attacking the player");
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
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Gizmos.DrawWireCube(spawnAnchor.position, new Vector3(0.1f, 0.1f, 0.1f));
        // Gizmos.DrawIcon(anchor.position, "Light Gizmo.tiff", true);
    }
}
