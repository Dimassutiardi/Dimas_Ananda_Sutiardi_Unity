using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float screenLimitY;
    private bool movingDown;

    void Start()
    {
        // Mendapatkan batas layar pada sumbu y berdasarkan ukuran kamera
        screenLimitY = Camera.main.orthographicSize;

        // Spawn di bagian atas layar dengan posisi x acak
        transform.position = new Vector2(Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), screenLimitY);
        movingDown = true; // Mulai bergerak ke bawah

        // Pastikan objek tidak berputar dan tidak terpengaruh gravitasi
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.freezeRotation = true; // Mencegah rotasi karena fisika
            rb.gravityScale = 0; // Mencegah objek jatuh
        }
    }

    void Update()
    {
        // Tetapkan arah hanya pada sumbu y
        direction = movingDown ? Vector2.down : Vector2.up;
        Move();

        // Periksa jika Enemy sudah keluar batas layar dan ubah arah jika diperlukan
        if (movingDown && transform.position.y < -screenLimitY)
        {
            movingDown = false; // Ubah arah ke atas
        }
        else if (!movingDown && transform.position.y > screenLimitY)
        {
            movingDown = true; // Ubah arah ke bawah
        }
    }
}
