using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; 

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage;
    private Rigidbody2D rb;    

    public IObjectPool<Bullet> ObjectPool { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // HitboxComponent hitbox = collision.gameObject.GetComponent<HitboxComponent>();
        // if (hitbox != null)
        // {
        //     hitbox.Damage(damage);
        // }

        ObjectPool.Release(this);
    }

    void OnBecameInvisible()
    {
        // Return bullet to pool when it goes off-screen
        ObjectPool.Release(this);
    }

    public void ResetBullet()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    public void Initialize()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        ResetBullet();
    }
}