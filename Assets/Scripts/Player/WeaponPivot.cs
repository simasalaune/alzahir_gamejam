using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private float orbitDistance = 3.0f;

    private void Start()
    {
        playerTransform = transform.parent.gameObject.transform;
    }

    private void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerTransform.position;
        direction.z = 0f;
        transform.position = playerTransform.position + direction.normalized * orbitDistance;
    }
}
