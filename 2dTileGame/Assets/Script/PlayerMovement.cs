using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    [SerializeField] float speed = 5f;
    Rigidbody2D rigid;
    Vector2 moveInput;
    [SerializeField] float jumpSpeed=5f;
    [SerializeField] float climbSpeed = 5f;
    CapsuleCollider2D capsule;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rigid.gravityScale=8;
        myFeetCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {

        if(!isAlive) { return; }
        //float x = Input.GetAxis("Horizontal");
        Run();
        FlipSprite();
        ClimbingLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
       
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!capsule.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if(value.isPressed)
        {
            rigid.velocity += new Vector2(0f, jumpSpeed);
        }

    }
    void Run ()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*speed,rigid.velocity.y);
        rigid.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRuning", playerHasHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigid.velocity.x), 1f);
        }
    }

   void ClimbingLadder()
    {


        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigid.gravityScale = gravityScaleAtStart;
            anim.SetBool("isWallJumping", false);
            return;

        }

            Vector2 ClimbVelocity = new Vector2(rigid.velocity.x,moveInput.y*climbSpeed);

       
        rigid.velocity = ClimbVelocity;
        rigid.gravityScale = 0f;
        

    }


    void Die()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
        }
    }

}
