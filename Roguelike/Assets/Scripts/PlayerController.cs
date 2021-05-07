using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float speed = 10;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x!=0)
        {
            transform.localScale=new Vector3(movement.x,1,1);
        }
        SwichAnim();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("Door"))
        //{
        //    collision.transform.parent.GetComponent<Room>().SetDoorOpen();
        //}
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void SwichAnim()
    {
        animator.SetFloat("speed",movement.magnitude);
    }
}
