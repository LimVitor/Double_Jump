using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    float dir, attackTime;
    private bool isOnGround, isAttacking;
    public bool doubleJump;

    [SerializeField]
    private float jumpForce = 700;
    [SerializeField]
    private float maxSpeedX = 2;
    [SerializeField]
    private float acceleration = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (!isAttacking)
        { 
            dir = Input.GetAxis("Horizontal");
            //Vector2.right = new Vector2(1, 0);
            rb.AddForce(Vector2.right * dir * acceleration);
        }

        if (rb.velocity.x > maxSpeedX) rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
        if (rb.velocity.x < -maxSpeedX) rb.velocity = new Vector2(-maxSpeedX, rb.velocity.y);

        if (rb.velocity.x < 0) transform.localScale = new Vector2(-1, 1);
        if (rb.velocity.x > 0) transform.localScale = new Vector2(+1, 1);

       

        if(Input.GetButtonDown("Attack") && isOnGround && !isAttacking)
        {
            //12s de jogo + attackTime = 13
            attackTime = Time.time + .5f;
            isAttacking = true;
        }
      
     

        if (attackTime < Time.time) isAttacking = false;

        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround)

            {

                rb.AddForce(Vector2.up * jumpForce);
                doubleJump = true;

            }
            else
            {

                if (doubleJump)
                {
                   
                    rb.AddForce(Vector2.up * jumpForce / 4);
                    doubleJump = false;
                }


            }
        }

        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("speedY", rb.velocity.y);
        anim.SetBool("onGround", isOnGround);
        anim.SetBool("attack", isAttacking);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    
        {
            if (collision.CompareTag("Ground"))
            {
                isOnGround = false;
            }
        }
    
}
