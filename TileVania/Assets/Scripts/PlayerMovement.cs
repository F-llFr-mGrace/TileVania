using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltrunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 10f;
    [SerializeField] float fltclimbSpeed = 5f;
    Vector2 v2moveInupt;
    Rigidbody2D rbMyRigidBody;
    Animator amMyAnimator;
    CapsuleCollider2D CCMyCapsuleCollider;
    float fltgravityScaleAtStart;

    private void Start()
    {
        rbMyRigidBody = GetComponent<Rigidbody2D>();
        amMyAnimator = GetComponent<Animator>();
        CCMyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        fltgravityScaleAtStart = rbMyRigidBody.gravityScale;
    }
    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        if (!CCMyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rbMyRigidBody.gravityScale = fltgravityScaleAtStart;
            amMyAnimator.SetBool("isClimbing", false);
            return;
        }
        
        Vector2 v2ClimbVelocity = new Vector2(rbMyRigidBody.velocity.x, v2moveInupt.y * fltclimbSpeed);
        rbMyRigidBody.velocity = v2ClimbVelocity;
        rbMyRigidBody.gravityScale = 0f;

        bool boolPlayerHasVerticalSpeed = Mathf.Abs(rbMyRigidBody.velocity.y) > Mathf.Epsilon;
        amMyAnimator.SetBool("isClimbing", boolPlayerHasVerticalSpeed);
    }

    private void FlipSprite()
    {
        bool boolPlayerHasHorizontalSpeed = Mathf.Abs(rbMyRigidBody.velocity.x) > Mathf.Epsilon;

        if (boolPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rbMyRigidBody.velocity.x), 1f);
        }
    }

    private void Run()
    {
        Vector2 v2playerVelocity = new Vector2 (v2moveInupt.x * fltrunSpeed, rbMyRigidBody.velocity.y);
        rbMyRigidBody.velocity = v2playerVelocity;

        bool boolPlayerHasHorizontalSpeed = Mathf.Abs(rbMyRigidBody.velocity.x) > Mathf.Epsilon;
        amMyAnimator.SetBool("isRunning", boolPlayerHasHorizontalSpeed);
    }

    private void OnMove (InputValue value)
    {
        v2moveInupt = value.Get<Vector2>();
        Debug.Log(v2moveInupt);
    }

    private void OnJump(InputValue value)
    {
        if (!CCMyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            //do stuff
            rbMyRigidBody.velocity += new Vector2(0f, fltJumpSpeed);
        }
    }
}
