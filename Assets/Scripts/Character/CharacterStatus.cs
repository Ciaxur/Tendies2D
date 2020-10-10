using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CharacterStatus : MonoBehaviour {
    // Structures & Types
    struct PowerUpStat {
        public PowerUp attack;
        public PowerUp defense;
        public PowerUp speed;
        public PowerUp vibe;
    }
        
    
    // Public Settings
    public int maxHealth = 10;
    public int scorePoints = 10;    // Points on Death

    // Drop Chance
    public float chanceOfDrop = 0.01f;  // Enemies-Only
    public GameObject heartDrop;
    
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

        // Enemy Died, add Score & Time
        if (gameObject.tag != "Player") {
            g.addScore(scorePoints);
            FindObjectOfType<GameTimer>().addToTime(GameTimer.TYPE.ENEMY_KILL);
        }
        
        // Chance of Drop after Death
        if (gameObject.tag != "Player" && Random.Range(0f, 1f) < chanceOfDrop) {
            Instantiate(heartDrop, transform.position, Quaternion.identity);
        }
        
        // Clean up
        Destroy(gameObject);
    }
    
    // POWER UP INFORMATION
    // Returns the Damage Buff Value
    public int getDamageBuff() {
        // Vibe Mode Supercedes
        if (powerups.vibe) {
            return powerups.vibe.buffAmount;
        }
        
        // Regular Attack Buff
        return powerups.attack ? powerups.attack.buffAmount : 1;
    }

    // Returns the Speed Buff Value
    public int getSpeedBuff() {
        return powerups.speed ? powerups.speed.buffAmount : 1;
    }

    // Returns the Stat of a Buff Type
    public bool hasBuff(PowerUp.BUFF_TYPE type) {
        switch(type) {
            case PowerUp.BUFF_TYPE.ATTACK:
                return powerups.attack;
            case PowerUp.BUFF_TYPE.DEFENSE:
                return powerups.defense;
            case PowerUp.BUFF_TYPE.SPEED:
                return powerups.speed;
            case PowerUp.BUFF_TYPE.VIBE:
                return powerups.vibe;
            default:
                return false;
        }
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
