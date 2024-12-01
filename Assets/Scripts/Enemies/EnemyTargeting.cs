using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform;
    private float speed = 2.0f; 
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

    }

    void Update()
    {
        // Jika Player ada, arahkan Enemy menuju posisi Player
        if (playerTransform != null)
        {
            // Hitung arah menuju Player dan normalisasi
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        } // Panggil Move() untuk menggerakkan Enemy ke arah Player
        
    }

    // Ketika bersentuhan dengan Player, hancurkan objek Enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Menghancurkan Enemy
        }
    }
}