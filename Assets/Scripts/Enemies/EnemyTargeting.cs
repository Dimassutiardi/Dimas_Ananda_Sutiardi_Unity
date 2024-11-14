using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform;

    void Start()
    {
        // Cari Player di scene berdasarkan tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please make sure there is an object with the 'Player' tag.");
        }

        // Pastikan objek tidak berputar dan tidak terpengaruh gravitasi
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.freezeRotation = true; // Mencegah rotasi
            rb.gravityScale = 0; // Mencegah objek jatuh
        }
    }

    void Update()
    {
        // Jika Player ada, arahkan Enemy menuju posisi Player
        if (playerTransform != null)
        {
            // Hitung arah menuju Player dan normalisasi
            direction = (playerTransform.position - transform.position).normalized;
            Move(); // Panggil Move() untuk menggerakkan Enemy ke arah Player
        }
    }

    // Ketika bersentuhan dengan Player, hancurkan objek Enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Menghancurkan Enemy
        }
    }
}