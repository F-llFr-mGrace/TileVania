using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed = 1f;
    Rigidbody2D rbMyRigidbody;

    private void Start()
    {
        rbMyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rbMyRigidbody.velocity = new Vector2 (fltMoveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        fltMoveSpeed = -fltMoveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2((-Mathf.Sign(rbMyRigidbody.velocity.x)), 1f);
    }
}
