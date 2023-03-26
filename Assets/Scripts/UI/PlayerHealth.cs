using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    [SerializeField]
    private float chipSpeed = 2f;

    private float health;
    private float lerpTimer = 0f;

    [SerializeField]
    private Image frontHealthBar;
    [SerializeField]
    private Image backHealthBar;

    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Subtract the damage from the current health

        if (health <= 0)
        {
            Die(); // Call the Die method when the object's health reaches zero or below
        }
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
    }

    public void FreezeFail()
    {
        health = maxHealth * 0.2f;
    }

    public void Die()
    {
        pauseMenu.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyAI>().damage);
        }
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            RestoreHealth(2);
        }
    }
}
