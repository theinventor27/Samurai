using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision){
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit");
            // Access the 'Attributes' script on the enemy GameObject
            PlayerAttributes playerAttributes = collision.gameObject.GetComponent<PlayerAttributes>();

            // Check if the 'Attributes' script is found
            if (playerAttributes != null)
            {
                // Deduct 10 points from the health variable
                playerAttributes.health -= 30;
            }
        }
    }
}
