using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    //Editor inputs
    [SerializeField] private float movAccel;
    [SerializeField] private float movDecel;
    [SerializeField] private float movSpeedMax;
    [SerializeField] private float movSpeed;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpVel;
    [SerializeField] private int airJumpCountMax;
    [SerializeField] private int airJumpCount;

    //Component Variables
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    //Movement Commands/variables
    private enum MovementState { idle, running, jumping, falling }

    //Inner code variables
    private float dirX = 0f; //Establishes direction of the player
    private bool jump = false;
    void Start()
    {
        //Assigns variables to relevant components
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //update for precision and logic
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (isGrounded())
        {
            jump = false;
            airJumpCount = airJumpCountMax;
        }
        if (Input.GetButtonDown("Jump"))
        { 
            jump = true;
        }
        if (jump && isGrounded())
        {
            canJump();
        }
        else if (jump && airJumpCount > 0)
        {
            canJump();
            airJumpCount--;
        }
    }

    //fixed for consistency
    private void FixedUpdate()
    {
        if (dirX != 0)
        {
            movSpeed += movAccel;
        }
        else
        {
            movSpeed -= movDecel;
        }
        rb.velocity = new Vector2(dirX * movSpeed, rb.velocity.y);
        movSpeed = Mathf.Clamp(movSpeed, 0f, movSpeedMax);
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    private void canJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVel);
        jump = false;
    }
}
