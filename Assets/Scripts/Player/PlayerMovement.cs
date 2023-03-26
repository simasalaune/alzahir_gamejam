using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The movement speed of the player

    private Rigidbody2D rb; // The Rigidbody component of the player
    private Vector2 movement; // The direction of movement of the player
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component of the player
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Get the horizontal movement input from the player
        movement.y = Input.GetAxisRaw("Vertical"); // Get the vertical movement input from the player

        if (movement.x < 0 && facingRight)
        {
            FlipPlayer();
        }
        // Flip the sprite if moving right
        else if (movement.x > 0 && !facingRight)
        {
            FlipPlayer();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized); // Move the player based on the movement direction and speed
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
