using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltrunSpeed = 10f;
    Vector2 v2moveInupt;
    Rigidbody2D rbMyRigidBody;
    Animator amMyAnimator;

    private void Start()
    {
        rbMyRigidBody = GetComponent<Rigidbody2D>();
        amMyAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        Run();
        FlipSprite();
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

    void OnMove (InputValue value)
    {
        v2moveInupt = value.Get<Vector2>();
        Debug.Log(v2moveInupt);
    }
}
