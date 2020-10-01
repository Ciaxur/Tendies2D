using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // Public Settings
    public int maxHealth = 10;
    
    // Public State
    public int health;          // Initial Character Health
    
    
    void LateUpdate() {
        // Check if Player Died
        if (health <= 0) {
            kill();
        }
    }

    void Start() {
        health = maxHealth;
    }


    // TODO: Play Death Sequence?
    public void kill() {
        Destroy(gameObject);
    }
    

    // Inflicts Damage on Character
    public void inflictDamage(int val) {
        health -= val;
    }

    // Set total Health
    public void setHealth(int newHealth) {
        this.health = Mathf.Clamp(newHealth, 0, maxHealth);
    }

    // Increases Health by Value (Contrain to Max Health)
    public void increaseHealth(int val) {
        this.health += Mathf.Clamp(val, 0, maxHealth);
    }
}
