using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private List<Detector> detectors;

    [SerializeField] private AIData aiData;

    [SerializeField] private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField] private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    // public UnityEvent OnAttackPressed;
    // public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField] private Vector2 movementInput;

    // [SerializeField] private ContextSolver movementDirectionSolver;

    bool following = false;

    private void Start() {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection() {
        foreach (Detector detector in detectors) {
            detector.Detect(aiData);
        }
    }

    // private IEnumerator ChaseAndAttack() {
    //     if (aiData.currentTarget == null) {
    //         //Stopping Logic
    //         Debug.Log("Stopping");
    //         movementInput = Vector2.zero;
    //         following = false;
    //         yield break;
    //     }
    //     else {
    //         float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

    //         if (distance < attackDistance) {
    //             //Attack logic
    //             movementInput = Vector2.zero;
    //             OnAttackPressed?.Invoke();
    //             yield return new WaitForSeconds(attackDelay);
    //             StartCoroutine(ChaseAndAttack());
    //         }
    //         else {
    //             //Chase logic
    //             movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
    //             yield return new WaitForSeconds(aiUpdateDelay);
    //             StartCoroutine(ChaseAndAttack());
    //         }

    //     }

    // }
}
