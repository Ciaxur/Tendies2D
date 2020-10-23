using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Types
    public enum BUFF_TYPE { ATTACK, SPEED, DEFENSE, VIBE };
    
    // Public Settings of a Powerup
    public int buffAmount = 1;                          // Amount of Buff to Deal
    public int secondaryBuffAmount = 1;                 // Optional Secondary Buff
    public float buffTimer = 5.0f;                      // How long will Buff Last
    public BUFF_TYPE type;

    // Private State
    GameObject attatchedTo;         // Object Picked up By
    World world;                    // The World
    bool isActive = false;

    

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

        // Obtain World
        world = FindObjectOfType<World>();
        if (!world) {
            Debug.LogError("No World Object Found");
            Debug.Break();
        }
    }

    // Keep track of Being Alive
    bool isCleanedUp = true;
    void Update() {
        if (isActive) {
            buffTimer -= Time.deltaTime;
            if (buffTimer < 0f) {
                // Deactivate State
                if (type == BUFF_TYPE.VIBE) {
                    deactivateVibeMode();
                }
                
                if (isCleanedUp) {
                    Destroy(gameObject);
                }
            }
        }
    }

    void FixedUpdate() {
        if (type == BUFF_TYPE.VIBE) {
            float currOrthoSize = Camera.main.orthographicSize;
            float desiredOrthoSize = originalOrthoSize + 4f;
            float dSmooth = 0.2f;

            // Reverting Camera Back
            if (!isCleanedUp) {
                Camera.main.orthographicSize = Mathf.Lerp(currOrthoSize, originalOrthoSize, dSmooth);
                if (Mathf.Round(currOrthoSize) <= originalOrthoSize) {
                    Camera.main.orthographicSize = originalOrthoSize;
                    isCleanedUp = true;
                    Destroy(gameObject);
                }
            }

            // Smooth Out Camera
            else if (isActive && currOrthoSize < desiredOrthoSize) {
                Camera.main.orthographicSize = Mathf.Lerp(currOrthoSize, desiredOrthoSize, dSmooth);
            }
        }

    }

    // VIBE STATE
    float originalJumpMultip;
    float originalOrthoSize;
    float origDistTillDeath;
    void activateVibeMode() {
        // Components
        Controller controller = attatchedTo.GetComponent<Controller>();
        PlayerSprite sprite = attatchedTo.GetComponent<PlayerSprite>();

        // Get Previous Data
        originalJumpMultip = controller.jumpForceMultiplier;

        // Give Player Jump Boost
        controller.jumpForceMultiplier *= buffAmount;

        // Store Camera Information
        originalOrthoSize = Camera.main.orthographicSize;

        // Apply Distance till death mutliplier
        origDistTillDeath = world.playerDistTillDeath;
        float newDeathDist = origDistTillDeath * 2f;
        world.playerDistTillDeath = newDeathDist;

        // Configure Aesthetics
        sprite.setVibeMode();
    }

    void deactivateVibeMode() {
        // Components
        Controller controller = attatchedTo.GetComponent<Controller>();
        PlayerSprite sprite = attatchedTo.GetComponent<PlayerSprite>();

        // Restore Values
        // Camera.main.orthographicSize = originalOrthoSize;
        controller.jumpForceMultiplier = originalJumpMultip;
        sprite.setDefault();

        // Revert Distance till death mutliplier
        world.playerDistTillDeath = origDistTillDeath;

        // Still Need Cleaning up (Revert Camera)
        isCleanedUp = false;
    }

    // Supply Power Up to Stats
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {     // Only for Players
            // Get the Stat of Player
            CharacterStatus stats = other.gameObject.GetComponent<CharacterStatus>();
            if (stats) {
                // Apply the Power and Activate    
                if (stats.applyPowerUp(this)) {
                    // Disable Everything
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;

                    // Attach Object
                    attatchedTo = other.gameObject;

                    // Activate Vibe?
                    if (type == BUFF_TYPE.VIBE) {
                        activateVibeMode();
                    }

                    isActive = true;
                }
            }
        }
    }
}
