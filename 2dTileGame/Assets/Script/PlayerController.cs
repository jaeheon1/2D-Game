using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rigid;
    [SerializeField] float jumpPower = 1.0f;
    private float movingInput;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] CapsuleCollider2D capsule;
    [SerializeField] Animator anim;

    [SerializeField] int jumpCount = 2; // а║га х╫╪Ж
    private bool IsJumping;
    public bool isGrounded = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        IsJumping = false;
        jumpCount = 0;
    }

  
    void Update()
    {
        Jump();

        Move();
    }


    public void Jump()
    {

        if (jumpCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {


                IsJumping = true;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumpCount--;
               
            }
            else
            {
                return;
            }

        }

    }
     
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Ground"))
        {
           
            IsJumping = false;
            jumpCount = 2;
        }
    }
    public void Move()
    {

        float x = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(Speed * x, rigid.velocity.y);


        if (x > 0)
        {
            sprite.flipX = false;
        }
        else if (x < 0)
        {
            sprite.flipX = true;
        }


    }
}
