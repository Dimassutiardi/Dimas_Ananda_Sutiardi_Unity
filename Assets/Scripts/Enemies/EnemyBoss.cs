using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyBoss : Enemy
{
    private float screenLimitX;
    private bool movingRight;

    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    private IObjectPool<Bullet> objectPool;

    void Start()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnBulletGet,
            OnBulletRelease,
            OnBulletDestroy,
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 100
        );
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
    private void Shoot()
    {
        Bullet newBullet = objectPool.Get();
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private Bullet CreateBullet()
    {
        return Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    private void OnBulletGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnBulletRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnBulletDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}