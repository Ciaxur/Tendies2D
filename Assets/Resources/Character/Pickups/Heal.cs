using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    // External Settings
    public int healthAmount = 1;

    void Start() {
        // Ignore Enemies
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Pickups"), 
            LayerMask.NameToLayer("Enemy")
        );

        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Pickups"), 
            LayerMask.NameToLayer("Default")
        );
    }

    // Supply Power Up to Stats
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {     // Only for Players
            // Get the Stat of Player & Add Health
            CharacterStatus stats = collision.gameObject.GetComponent<CharacterStatus>();
            if(stats) {
                stats.increaseHealth(healthAmount);
            }

            // Clean Up
            Destroy(gameObject);
        }
    }
}
