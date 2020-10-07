using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Structures & Types
struct PowerUpStat {
    public PowerUp attack;
    public PowerUp defense;
    public PowerUp speed;
    public PowerUp vibe;
}

public class CharacterStatus : MonoBehaviour {
    // Public Settings
    public int maxHealth = 10;
    public int scorePoints = 10;    // Points on Death
    
    // Public State
    public int health;              // Initial Character Health

    // Private Power Up State
    private PowerUpStat powerups = new PowerUpStat();

    
    
    void LateUpdate() {
        // Check if Player Died
        if (health <= 0) {
            kill();
        }
    }

    void Start() {
        health = maxHealth;
    }


    // Play Death Sequence?
    public void kill() {
        // Find the World & Increase Score
        World g = FindObjectOfType<World>();
        g.addScore(scorePoints);
        
        // Clean up
        Destroy(gameObject);
    }
    
    // Returns the Damage Buff Value
    public int getDamageBuff() {
        return powerups.attack ? powerups.attack.buffAmount : 1;
    }

    // Returns the Speed Buff Value
    public int getSpeedBuff() {
        return powerups.speed ? powerups.speed.buffAmount : 1;
    }
    

    // Inflicts Damage on Character
    public void inflictDamage(int val) {
        health -= val / (powerups.defense ? powerups.defense.buffAmount : 1);
    }

    // Set total Health
    public void setHealth(int newHealth) {
        this.health = Mathf.Clamp(newHealth, 0, maxHealth);
    }

    // Increases Health by Value (Contrain to Max Health)
    public void increaseHealth(int val) {
        this.health += Mathf.Clamp(val, 0, maxHealth);
    }


    /**
     * Applies Power up to Stats if 
     *  there isn't one applied
     * 
     * @param powerup Power Up to Apply
     * @returns State of Power Up applying
     */
    public bool applyPowerUp(PowerUp powerup) {
        // Set Powerup
        switch(powerup.type) {
            case PowerUp.BUFF_TYPE.ATTACK:
                if (!powerups.attack) {     // Only apply if None Set
                    powerups.attack = powerup;
                    return true;
                }
                break;
            case PowerUp.BUFF_TYPE.DEFENSE:
                if (!powerups.defense) {
                    powerups.defense = powerup;
                    return true;
                }
                break;
            case PowerUp.BUFF_TYPE.SPEED:
                if (!powerups.speed) {
                    powerups.speed = powerup;
                    return true;
                }
                break;
            case PowerUp.BUFF_TYPE.VIBE:
                if (!powerups.vibe) {
                    powerups.vibe = powerup;
                    return true;
                }
                break;
            default:
                Debug.Log("Character Stat: Unknown Power-Up");
                break;
        }

        // Power Up not applied
        return false;
    }
}
