using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private string nameProjectile;

    [SerializeField] private float speed = 10f; // The speed at which the projectile moves
    [SerializeField] private float lifetime = 2f; // The lifetime of the projectile in seconds

    public float cd;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(nameProjectile == "Projectile1")
            if (collision.gameObject.CompareTag("Enemy"))
                Destroy(gameObject);

    }
    
}
