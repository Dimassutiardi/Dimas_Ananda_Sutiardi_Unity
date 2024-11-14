using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHorizontal : Enemy
{
    private float screenLimitX;
    private bool movingRight;

    void Start()
    {
        // Ambil batas layar berdasarkan ukuran kamera (hanya pada sumbu x)
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenLimitX = screenWidth;

        // Spawn acak dari sisi kiri atau kanan layar dan hanya bergerak pada sumbu x
        if (Random.value > 0.5f)
        {
            // Spawn dari sisi kiri layar
            transform.position = new Vector2(-screenLimitX, transform.position.y);
            movingRight = true;
        }
        else
        {
            // Spawn dari sisi kanan layar
            transform.position = new Vector2(screenLimitX, transform.position.y);
            movingRight = false;
        }

        // Pastikan objek tidak berputar
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.freezeRotation = true; // Mencegah rotasi karena fisika
            rb.gravityScale = 0; // Mencegah objek jatuh
        }
    }

    void Update()
    {
        // Tetapkan arah hanya pada sumbu x
        direction = movingRight ? Vector2.right : Vector2.left;
        Move();

        // Periksa batas layar, jika keluar, balik arah
        if (movingRight && transform.position.x > screenLimitX)
        {
            movingRight = false; // Berbalik ke kiri
        }
        else if (!movingRight && transform.position.x < -screenLimitX)
        {
            movingRight = true; // Berbalik ke kanan
        }
    }
}