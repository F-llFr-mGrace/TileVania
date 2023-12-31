using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject GoSelf;
    [SerializeField] Transform TfGun;
    [SerializeField] GameObject GoBullet;
    [SerializeField] float fltrunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 10f;
    [SerializeField] float fltclimbSpeed = 5f;
    Vector2 v2moveInupt;
    Rigidbody2D rbMyRigidBody;
    Animator amMyAnimator;
    CapsuleCollider2D ccMyBodyCollider;
    BoxCollider2D bcMyFootCollider;
    float fltgravityScaleAtStart;
    bool boolIsAlive = true;

    private void Start()
    {
        rbMyRigidBody = GetComponent<Rigidbody2D>();
        amMyAnimator = GetComponent<Animator>();
        ccMyBodyCollider = GetComponent<CapsuleCollider2D>();
        bcMyFootCollider = GetComponent<BoxCollider2D>();
        fltgravityScaleAtStart = rbMyRigidBody.gravityScale;
    }
    private void Update()
    {
        if (!boolIsAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void Die()
    {
        if (ccMyBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            boolIsAlive = false;
            amMyAnimator.SetTrigger("Dying");
            GoSelf.layer = 12;
            rbMyRigidBody.velocity = new Vector2(0,10);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void ClimbLadder()
    {
        if (!bcMyFootCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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
        if (!boolIsAlive) { return; }
        v2moveInupt = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!boolIsAlive) { return; }
        if (!bcMyFootCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            //do stuff
            rbMyRigidBody.velocity += new Vector2(0f, fltJumpSpeed);
        }
    }

    private void OnFire()
    {
        if (!boolIsAlive) { return; }
        Instantiate(GoBullet, TfGun.position, transform.rotation);
    }
}
