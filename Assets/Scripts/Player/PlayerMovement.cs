using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [Header ("Movement Parameter")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header ("Coyote Time")]
    [SerializeField] private float coyoteMaxTime; // the max time for the player can be in the air before jumping
    private float coyoteTimeCounter; // how much time have passed since the player is in the air

    [Header ("Layer")]
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

        
        //FormerJumpLogic();

        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height, when you release the scpace button, the jump get less higher
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y /2);

        if(onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
             body.gravityScale = 1.75f; // the origal gravity scale on boxcollider component
             body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            // Coyote logic
            if (isGrounded())
            {
                coyoteTimeCounter = coyoteMaxTime; // reset coyote time
            }
            else
                coyoteTimeCounter -= Time.deltaTime;
        }

        // TO DELETE
        changeScene();
    }

    private void FormerJumpLogic()
    {
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
        if(coyoteTimeCounter <=0 && !onWall()) return; // if the player is in the air too much time, he cannot jump

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
            {  
                body.velocity= new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(coyoteTimeCounter > 0)
                {
                   body.velocity= new Vector2(body.velocity.x, jumpPower); 
                }
            }

            // prevent to make double coyote jump
            coyoteTimeCounter = 0;
        }
        
        //  former jump logic 
        // if (isGrounded())
        // {  // normal jump
        //     body.velocity = new Vector2(body.velocity.x, jumpPower);
        //     //anim.SetTrigger("jump");
        // }
        // else if (onWall() && !isGrounded())
        // {  // wall jump case
        //     if (!isMoving())
        //     {
        //         body.velocity = new Vector2(-MathF.Sign(transform.localScale.x) * speed, 0);
        //         transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //     }
        //     else
        //         body.velocity = new Vector2(-MathF.Sign(transform.localScale.x) * speed / 3, jumpPower / 2);

        //     wallJumpCooldown = 0;
        // }


    }

    private void WallJump()
    {
        
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


    // for testing , allow to change scene
    private void changeScene()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(0);
        }
    }

}
