using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D player;
    BoxCollider2D collider2D;

    [Header("移动参数")]
    public float speed = 8f;//移动速度
    public float crouchSpeed = 3f;//蹲下后移动速度


    [Header("跳跃参数")]
    public float jumpForce = 6.3f;//跳跃的力
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;//计算跳起来的时间差
    public float crounchJumpBoost = 2.5f;//蹲下跳跃的力

    float jumpTime;

    [Header("状态")]
    public bool isCrounch;///是否蹲下
    public bool isOnGround;///是否在地面
    public bool isJump;//是否跳跃

    [Header("环境监测")]
    public LayerMask groundLayer;

    //按键设置
    bool jumpPressed;//按下跳跃
    bool jumpHeld;//按跳跃
    bool crouchHeld;//按下蹲

    //碰撞体尺寸
    Vector2 colliderStandSiza;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSiza;
    Vector2 colliderCrouchOffset;

    float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();

        colliderStandSiza = collider2D.size;
        colliderStandOffset = collider2D.offset;
        colliderCrouchSiza = new Vector2(collider2D.size.x, collider2D.size.y / 2);
        colliderCrouchOffset = new Vector2(collider2D.offset.x, collider2D.offset.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovementt();
        MidAirMovement();
    }
    void PhysicsCheck()
    {
        if (collider2D.IsTouchingLayers(groundLayer))
            isOnGround = true;
        else
            isOnGround = false;

    }
    void GroundMovementt()
    {
        if (crouchHeld && !isCrounch  && isOnGround)
        {
            Crouch();
        }
        else if (!crouchHeld && isCrounch)
        {
            StanUp();
        }
        else if (!isOnGround && isCrounch)
        {
            StanUp();
        }
        xVelocity = Input.GetAxis("Horizontal");
        if (isCrounch)
            xVelocity /= crouchSpeed;
        player.velocity = new Vector2(xVelocity * speed, player.velocity.y);
        FilpDirction();
    }
    void FilpDirction()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-1, 1);
        if (xVelocity > 0)
            transform.localScale = new Vector2(1, 1);
    }
    void MidAirMovement()
    {
        if (jumpPressed && isOnGround && !isJump)
        {
            if (isCrounch && isOnGround)
            {
                StanUp();
                player.AddForce(new Vector2(0, crounchJumpBoost), ForceMode2D.Impulse);
            }
            isOnGround = false;
            isJump = true;

            jumpTime = Time.time + jumpHoldDuration;

            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (isJump)
        {
            if (jumpHeld)
            {
                player.AddForce(new Vector2(0, jumpHoldForce), ForceMode2D.Impulse);
            }
            if (jumpTime < Time.time)
            {
                isJump = false;
            }
        }
    }
    void Crouch()
    {
        isCrounch = true;
        collider2D.size = colliderCrouchSiza;
        collider2D.offset = colliderCrouchOffset;
    }
    void StanUp()
    {
        isCrounch = false;
        collider2D.size = colliderStandSiza;
        collider2D.offset = colliderStandOffset;
    }
}
