using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5f; // The maximum health of the object
    private float currentHealth; // The current health of the object

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to the maximum health when the object is created
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Subtract the damage from the current health

        if (currentHealth <= 0)
        {
            Die(); // Call the Die method when the object's health reaches zero or below
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(2);
        }
    }
}
