using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] projectilePrefabs; // The projectile prefab to be instantiated
    [SerializeField]
    private GameObject projectilePrefab; // The projectile prefab to be instantiated
    [SerializeField]
    private Transform projectileSpawnPoint; // The spawn point of the projectile
    [SerializeField] private Animator anim;

    public int currentHead;
    private float coolDown = 0.5f;


    void Update()
    {
        if (coolDown <= 0 && Input.GetButtonDown("Fire1")) // Check if the left mouse button is clicked
        {
            Shoot(); // Call the Shoot method to shoot a projectile
            anim.SetTrigger("Shoot");
            coolDown = projectilePrefab.GetComponent<Projectile>().cd;
        }
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen space
        mousePosition.z = Camera.main.transform.position.z; // Set the mouse position to the same z-axis as the camera
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convert the mouse position to world space
        spawnPosition.z = 0; // Set the z-axis of the spawn position to zero

        // Instantiate the projectile at the spawn point with the correct rotation
        Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

    }
    public void ChangeProjectile()
    {
        currentHead = Random.Range(0, projectilePrefabs.Length);
        projectilePrefab = projectilePrefabs[currentHead];
    }
}
