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

    private void Start()
    {
        rbMyRigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        Vector2 v2playerVelocity = new Vector2 (v2moveInupt.x * fltrunSpeed, rbMyRigidBody.velocity.y);
        rbMyRigidBody.velocity = v2playerVelocity;
    }

    void OnMove (InputValue value)
    {
        v2moveInupt = value.Get<Vector2>();
        Debug.Log(v2moveInupt);
    }
}
