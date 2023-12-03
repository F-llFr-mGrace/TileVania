using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbBullet;
    [SerializeField] float fltBulletSpeed;
    PlayerMovement PmPlayer;
    float xSpeed;

    private void Start()
    {
        PmPlayer = FindObjectOfType<PlayerMovement>();
        xSpeed = PmPlayer.transform.localScale.x * fltBulletSpeed;
    }

    private void Update()
    {
        rbBullet.velocity = new Vector2(xSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy-Bullet collision");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
