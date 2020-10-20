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
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {     // Only for Players
            // Get the Stat of Player & Add Health
            CharacterStatus stats = other.gameObject.GetComponent<CharacterStatus>();
            if(stats) {
                stats.increaseHealth(healthAmount);
            }

            // Clean Up
            Destroy(gameObject);
        }
    }
}
