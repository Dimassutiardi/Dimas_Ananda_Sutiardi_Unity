using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] public Weapon weaponHolder;

    private Weapon weapon;

    private void Awake()
    {
        // Initialize the weapon with the weaponHolder
        weapon = Instantiate(weaponHolder);
    }

    private void Start()
    {
        // If weapon is not null, disable its visuals
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if (playerWeapon != null)
            {
                playerWeapon.transform.SetParent(playerWeapon.parentTransform);
                playerWeapon.transform.localScale = new(1, 1);
                playerWeapon.transform.localPosition = new(0, 0);

                TurnVisual(false, playerWeapon);
            }

            weapon.enabled = true;
            weapon.transform.SetParent(other.transform, false);

            TurnVisual(true);

            weapon.transform.localPosition = new(0.0f, 0.0f);
        }
    }

    // TurnVisual method without parameters for toggling the visual state of the weapon
    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    // Overloaded TurnVisual method with weapon parameter for polymorphic behavior
    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
