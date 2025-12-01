using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header ("Sound")]
    [SerializeField] private AudioClip jumpSound;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float HorizontalInput;


    private void Awake()
    {
        // get Component of the GameObject
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        // flip character sprite when moving left or right
        if (HorizontalInput > 0.1f)
            transform.localScale = Vector3.one;
        else if (HorizontalInput < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);

        //animation parameter
        anim.SetBool("run", isMoving());
        anim.SetBool("grounded", isGrounded());


        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1.75f; // the origal gravity scale on boxcollider component

            // jump
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() || onWall()))
                {
                    SoundManager.instance.PlaySound(jumpSound);   
                }
            }
                
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {  // normal jump
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {  // wall jump case
            if (!isMoving())
            {
                body.velocity = new Vector2(-MathF.Sign(transform.localScale.x) * speed, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-MathF.Sign(transform.localScale.x) * speed / 3, jumpPower / 2);

            wallJumpCooldown = 0;
        }


    }

    private bool isMoving()
    {
        return HorizontalInput != 0;
    }

    // maybe add the content of the method for the knowledge
    // replace by isGrounded() and onWall()
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        // if the rayCast is not in collision with a GameObject with layer ground, the collider is null
        return rayCastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        // if the rayCast is not in collision with a GameObject with layer wall, the collider is null
        return rayCastHit.collider != null;
    }

    public bool canAttack()
    {
        return !isMoving() && isGrounded() && !onWall();
    }

}
