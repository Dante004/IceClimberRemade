using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private float groundRadius = 0.2f;
    public bool grounded;
    public float speed;
    public float jumpForce;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float jumpTime;
    public float jumpTimeCounter;
    public bool stoppedJumping;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }

    private void FixedUpdate()
    {
        Jump();

        Movement();
    }

    private void Jump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("ySpeed", rb2d.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                stoppedJumping = false;
                anim.SetBool("Ground", false);
            }
        }

        if (Input.GetKey(KeyCode.Space) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Run", Mathf.Abs(h) > 0);
        rb2d.velocity = new Vector2(h * speed, rb2d.velocity.y);
        if (h > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}