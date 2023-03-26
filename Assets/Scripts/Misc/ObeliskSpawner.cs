using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obeliskPrefab;
    [SerializeField] private int numberOfObelisks = 10;
    [SerializeField] private float spawnRadius = 50f;

    private void Start()
    {
        for (int i = 0; i < numberOfObelisks; i++)
        {
            Vector2 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(obeliskPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
