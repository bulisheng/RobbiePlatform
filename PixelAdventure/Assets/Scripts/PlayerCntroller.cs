using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntroller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed;
    float xVelocoty;

    public float checkRadius;
    public LayerMask plattform;
    public GameObject groundCheck;
    bool isOnGround;//是否在平台上
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, checkRadius, plattform);
        anim.SetBool("isOnGround", isOnGround);
        Movement();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fan"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spiked"))
        {
            anim.SetTrigger("dead");
            GameManager.GameOver(true);
        }
    }
    void Movement()
    {
        xVelocoty = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xVelocoty * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (xVelocoty != 0)
        {
            transform.localScale = new Vector3(xVelocoty, 1, 1);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkRadius);
    }
}
