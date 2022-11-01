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
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        Jump();

        Move();
    }


    public void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isWallJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
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
