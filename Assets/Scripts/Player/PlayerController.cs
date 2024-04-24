using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance {get; private set;}
    public float speed = 5f;
    private Vector2 movement;
    private Rigidbody2D body;
    private Animator animator;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashSpeed = 15f;
    private float dashDuration = 0.1f;
    public float dashCooldown = 1f;

    public bool invulnerable = false;

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

        if (canDash && Input.GetButtonDown("Jump") && movement!= Vector2.zero) StartCoroutine(Dash());

        // if (canAttack && Input.GetKeyDown(KeyCode.Mouse0)) animator.SetTrigger("attack");
    }

    private IEnumerator Dash() {
        isDashing = true;
        canDash = false;
        invulnerable = true;

        body.velocity = movement * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        invulnerable = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
