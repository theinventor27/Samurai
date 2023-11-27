using UnityEngine;

public class AttackHitbox : MonoBehaviour

{

    private AudioSource audioSource;
    public AudioClip swordSlash_NoHit;
    public AudioClip swordSlash_Hit;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
  
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {

            audioSource.clip = swordSlash_Hit;
            audioSource.volume = .2f;
            audioSource.Play();
            // Access the 'Attributes' script on the enemy GameObject
            HandleDeath enemyAttributes = collision.gameObject.GetComponent<HandleDeath>();

            // Check if the 'Attributes' script is found
            if (enemyAttributes != null)
            {
                // Deduct 10 points from the health variable
                enemyAttributes.Health -= 100;

                

            }

        }
        else
        {
            audioSource.clip = swordSlash_NoHit;
            audioSource.Play();
        }

    }
}
