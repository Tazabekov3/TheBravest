using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    private Vector2 movement;
    private Rigidbody2D body;
    private Animator animator;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashSpeed = 15f;
    private float dashDuration = 0.1f;
    private float dashCooldown = 1f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private float attackCooldown = 0.2f;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetBool("isDashing", isDashing);
        if (isDashing) return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        body.velocity = movement * speed;

        if (moveHorizontal > 0.01f) transform.localScale = Vector3.one;
        else if (moveHorizontal < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("isMoving", movement != Vector2.zero);

        if (canDash && Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Dash());

        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0)) animator.SetTrigger("attack");
    }

    private IEnumerator Dash() {
        isDashing = true;
        canDash = false;
        body.velocity = movement * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
