using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float smoothSpeed = 0.125f; // The speed at which the camera follows the target

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 0);
    private Transform target; // The target to follow

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed*Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
