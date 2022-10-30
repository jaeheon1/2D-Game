using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.velocity = new Vector2(moveSpeed, 0f);

        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FilpEnemyFacing();

    }

   void FilpEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigid.velocity.x)), 1f);
    }
}
