using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * speed;
        body.velocity = movement;

        if (moveHorizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (moveHorizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("isMoving", movement != Vector2.zero);
    }
}
