using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMove playerMove;
    Rigidbody2D rb;
    int groundID;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponentInParent<PlayerMove>();
        rb = GetComponentInParent<Rigidbody2D>();
        groundID = Animator.StringToHash("isOnGround");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(playerMove.xVelocity));
        animator.SetBool("isCrouching", playerMove.isCrounch);
        animator.SetBool("isHanging", playerMove.isHanging);
        animator.SetBool("isJumping", playerMove.isJump);
        //用编号传动画状态
        //animator.SetBool("isOnGround", playerMove.isOnGround);
        animator.SetBool(groundID, playerMove.isOnGround);

        animator.SetFloat("verticalVelocity", rb.velocity.y);
    }
    public void StepAudio()
    {
        AudioManager.PlayFootStepAudio();
    }
    public void CrouchStepAudio()
    {
        AudioManager.PlayCrouchStepAudio();
    }

}
