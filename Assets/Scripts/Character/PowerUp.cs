using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Types
    public enum BUFF_TYPE { ATTACK, SPEED, DEFENSE, VIBE };

    
    // Public Settings of a Powerup
    public int buffAmount = 1;                          // Amount of Buff to Deal
    public float buffTimer = 5.0f;                      // How long will Buff Last
    public BUFF_TYPE type;

    // Private State
    private bool isActive = false;


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

    // Keep track of Being Alive
    void Update() {
        if (isActive) {
            buffTimer -= Time.deltaTime;
            if (buffTimer < 0) {
                Destroy(gameObject);
            }
        }
    }

    // Supply Power Up to Stats
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {     // Only for Players
            // Get the Stat of Player
            CharacterStatus stats = collision.gameObject.GetComponent<CharacterStatus>();
            if(stats) {
                // Apply the Power and Activate    
                if (stats.applyPowerUp(this)) {
                    // Disable Everything
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;

                    isActive = true;
                }
            }
        }
    }
}
