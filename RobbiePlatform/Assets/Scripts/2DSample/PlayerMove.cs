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
    public float haningJumpForce = 15f;//挂着时候的力

    float jumpTime;

    [Header("状态")]
    public bool isCrounch;///是否蹲下
    public bool isOnGround;///是否在地面
    public bool isJump;//是否跳跃
    public bool isHeadBlocked;//头部是否撞上墙
    public bool isHanging;//是否悬挂

    [Header("环境监测")]
    public float footOffset = 0.4f;
    public float headClearance = 0.5f;
    public float grounddDistance = 0.2f;

    float playerHeight;//角色高度
    public float eyeHeight = 1.5f;//眼睛高度
    public float grabDistance = 0.4f;//人物距离墙的距离
    public float reachOffset = 0.7f;//头顶高度

    public LayerMask groundLayer;

    //按键设置
    bool jumpPressed;//按下跳跃
    bool jumpHeld;//按跳跃
    bool crouchHeld;//按下蹲
    bool crouPressed;//按下下蹲

    //碰撞体尺寸
    Vector2 colliderStandSiza;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSiza;
    Vector2 colliderCrouchOffset;

    public float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();

        colliderStandSiza = collider2D.size;
        colliderStandOffset = collider2D.offset;
        playerHeight = collider2D.size.y;
        colliderCrouchSiza = new Vector2(collider2D.size.x, collider2D.size.y / 2);
        colliderCrouchOffset = new Vector2(collider2D.offset.x, collider2D.offset.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
        crouPressed = Input.GetButtonDown("Crouch");
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovementt();
        MidAirMovement();
    }
    void PhysicsCheck()
    {

        //左右脚射线
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, grounddDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, grounddDistance, groundLayer);

        if (leftCheck || rightCheck)
            isOnGround = true;
        else
            isOnGround = false;
        //头顶射线
        RaycastHit2D headCheck = Raycast(new Vector2(0f, collider2D.size.y), Vector2.up, headClearance, groundLayer);
        if (headCheck)
        {
            isHeadBlocked = true;
        }
        else
        {
            isHeadBlocked = false;
        }

        float direction = transform.localScale.x;
        Vector2 grabDir = new Vector2(direction, 0);

        RaycastHit2D blockedCheck = Raycast(new Vector2(footOffset * direction, playerHeight), grabDir, grabDistance, groundLayer);
        RaycastHit2D walledCheck = Raycast(new Vector2(footOffset * direction, eyeHeight), grabDir, grabDistance, groundLayer);
        RaycastHit2D ledgeCheck = Raycast(new Vector2(reachOffset * direction, playerHeight), Vector2.down, grabDistance, groundLayer);
        if (!isOnGround && player.velocity.y < 0f && ledgeCheck && walledCheck && !blockedCheck)
        {      
            Vector3 pos = transform.position;
            Debug.LogError(walledCheck.distance);
            pos.x += (direction<0? -1:1)*walledCheck.distance - 0.05f * direction;
            pos.y -= ledgeCheck.distance;
            transform.position = pos;

            player.bodyType = RigidbodyType2D.Static;
            isHanging = true;
        }
    }
    void GroundMovementt()
    {
        if (isHanging)
        {
            return;//如果悬挂就不能左右旋转
        }
        if (crouchHeld && !isCrounch && isOnGround)
        {
            Crouch();
        }
        else if (!crouchHeld && isCrounch && !isHeadBlocked)
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
            transform.localScale = new Vector3(-1, 1,1);
        if (xVelocity > 0)
            transform.localScale = new Vector3(1, 1,1);
    }
    void MidAirMovement()
    {
        if (isHanging)
        {
            if (jumpPressed)
            {
                player.bodyType = RigidbodyType2D.Dynamic;
                player.velocity = new Vector2(player.velocity.x, haningJumpForce);
                isHanging = false;
            }
            if (crouPressed)
            {
                player.bodyType = RigidbodyType2D.Dynamic;
                isHanging = false;
            }
        }
        if (jumpPressed && isOnGround && !isJump && !isHeadBlocked)
        {
            if (isCrounch)
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
    RaycastHit2D Raycast(Vector2 offest, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offest, rayDiraction, length, layer);
        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offest, rayDiraction * length, color);//华射线
        return hit;
    }
}
