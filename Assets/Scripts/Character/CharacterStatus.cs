using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // Public Settings
    public int health = 10;     // Initial Character Health
    

    // Inflicts Damage on Character
    public void inflictDamage(int val) {
        health -= val;
    }
}
