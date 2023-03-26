using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float distanceBetween = 1f;

    public float damage = 1f;

    private Transform player;
    private float distance;
    private bool facingRight = false;

    [SerializeField]
    private Sprite spriteWhite;

    private Sprite newSprite;
    private Sprite originalSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        newSprite = GetComponent<SpriteRenderer>().sprite;
        originalSprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 movement = player.position - transform.position;
        GetComponent<SpriteRenderer>().sprite = newSprite;

        if (distance > distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        if (movement.x < 0 && facingRight)
        {
            FlipEnemy();
        }
        // Flip the sprite if moving right
        else if (movement.x > 0 && !facingRight)
        {
            FlipEnemy();
        }
    }
    private void FlipEnemy()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(FlashWhite());
        }
    }

    private IEnumerator FlashWhite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        var originalMaterial = spriteRenderer.material;

        var whiteMaterial = new Material(Shader.Find("GUI/Text Shader"));
        whiteMaterial.color = Color.white;

        spriteRenderer.material = whiteMaterial;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.material = originalMaterial;
    }
}
