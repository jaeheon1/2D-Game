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

    [SerializeField] int jumpCount = 2; // 점프 횟수
    
    public float wallCheckDistance;
    public float groundCheckDistance;
    private bool isWallDetected;

    private bool canWallSlide;
    private bool isWallSliding;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       
        jumpCount = 0;
    }

  
    void Update()
    {

        Move();
        Jump();

        WallCollisionChecks();

        



    }


    public void Jump()
    {

        if (jumpCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

              
             
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
                jumpCount--;
             
                if(jumpCount==0)
                {
                    anim.SetBool("isJumping", false);

                   
                }
              
            }
         

        }

    }
     
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Ground"))
        {
           
           
            jumpCount = 2;
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            OnDamaged(collision.transform.position);
        }
    }
    public void Move()
    {

        float x = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(Speed * x, rigid.velocity.y);

        bool isRunning = rigid.velocity.x != 0;


            anim.SetBool("isRunning", isRunning);
           

        



        if (x > 0)
        {
            sprite.flipX = false;
        }
        else if (x < 0)
        {
            sprite.flipX = true;
        }


    }
  
     public void Jumpanim()
    {
        //Landing Platform

       
            Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                {
                    Debug.Log(rayHit.collider.name);
                anim.SetBool("isJumping", false);
            }
            
             }
        
      }
    
    public  void WallCollisionChecks()
    {
        Debug.DrawRay(rigid.position, Vector2.right, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.right, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 1f)
            {
                Debug.Log(rayHit.collider.name);
               
            }

        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        //change Layer
        gameObject.layer = 13;

        //맞았을때 색 바꾸기 
        sprite.color = new Color(1, 1, 1, 0.4f);

        //맞았을때 팅기는 방향
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);
        Invoke("OffDamaged", 3);
    }
 
    void OffDamaged()
    {
        gameObject.layer = 8;
        sprite.color = new Color(1, 1, 1,1);

    }
}
