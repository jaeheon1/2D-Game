using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public float speed=1f;

    public float moveSpeed = 20f;

    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    void Start()
    {
        //transform.Rotate(0,0,45);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="speedup")
        {
            speed = boostSpeed;
            Debug.Log("스피드업");
        }

        if (other.tag == "things")
        {
            speed = slowSpeed;
            Debug.Log("스피드 다운");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (tag == "things")
        {
            speed = slowSpeed;
            Debug.Log("스피드 다운");
        }
    }
    void Update()
    {
        
        float x=Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        float y = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;
        transform.Rotate(0, 0,-x);
        transform.Translate(0,y,0);

    }
}
