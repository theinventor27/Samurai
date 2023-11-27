using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    public float lifetime = 3f; // Time in seconds before the fireball disappears

    void Start()
    {
        // Move the fireball forward
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Access the 'Attributes' script on the enemy GameObject
            Attributes enemyAttributes = collision.gameObject.GetComponent<Attributes>();

            // Check if the 'Attributes' script is found
            if (enemyAttributes != null)
            {
                // Deduct 10 points from the health variable
                enemyAttributes.health -= 10;

                // Optionally, check if health becomes zero and handle defeat or destruction of the enemy
                if (enemyAttributes.health <= 0)
                {
                    // Handle enemy defeat or destruction
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
