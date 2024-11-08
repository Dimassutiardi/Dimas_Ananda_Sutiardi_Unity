using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        weapon = weaponHolder;
    }

    void Start()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon holder is not assigned in WeaponPickup!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D triggered with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has collided with weapon pickup.");

            // Attach the weapon to the player and adjust its position
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;

            // Hide or remove the weapon pickup object
            HideWeaponPickup();
        }
    }

    private void HideWeaponPickup()
    {
        Debug.Log("Hiding weapon pickup...");

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.enabled = false;
            Debug.Log("SpriteRenderer disabled.");
        }

        if (TryGetComponent<Collider2D>(out Collider2D collider))
        {
            collider.enabled = false;
            Debug.Log("Collider disabled.");
        }

        Destroy(gameObject, 1f);
        Debug.Log("Weapon pickup GameObject will be destroyed after 1 second.");
    }
}

