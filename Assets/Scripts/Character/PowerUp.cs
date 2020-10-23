﻿using System.Collections;
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
    }

    // Keep track of Being Alive
    void Update() {
        if (isActive) {
            buffTimer -= Time.deltaTime;
            if (buffTimer < 0f) {
                // Deactivate State
                if (type == BUFF_TYPE.VIBE) {
                    deactivateVibeMode();
                }
                
                // Wait to Clean Up
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate() {
        if (type == BUFF_TYPE.VIBE) {
            float currOrthoSize = Camera.main.orthographicSize;
            float desiredOrthoSize = originalOrthoSize + 4f;
            float dSmooth = 0.2f;

            // Reverting Back
            if (isActive && currOrthoSize < desiredOrthoSize) {
                Camera.main.orthographicSize = Mathf.Lerp(currOrthoSize, desiredOrthoSize, dSmooth);
            }
        }

    }

    // VIBE STATE
    float originalJumpMultip;
    float originalOrthoSize;
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

        // Configure Aesthetics
        sprite.setVibeMode();
    }

    void deactivateVibeMode() {
        // Components
        Controller controller = attatchedTo.GetComponent<Controller>();
        PlayerSprite sprite = attatchedTo.GetComponent<PlayerSprite>();

        // Restore Values
        Camera.main.orthographicSize = originalOrthoSize;
        controller.jumpForceMultiplier = originalJumpMultip;
        sprite.setDefault();
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
