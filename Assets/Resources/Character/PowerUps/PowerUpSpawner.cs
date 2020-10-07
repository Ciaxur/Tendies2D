using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // External References to Power-Up Prefabs
    public GameObject attackPrefab;
    public GameObject defensePrefab;
    public GameObject speedPrefab;
    public GameObject vibePrefab;

    // Internal State
    private List<GameObject> powerups = new List<GameObject>();


    // Store all Powerups in List
    void Awake() {
        powerups = new List<GameObject>{
            attackPrefab,
            defensePrefab,
            speedPrefab,
            vibePrefab,
        };
    }
    
    // Return a Random Powerup
    public GameObject getPowerUp() {
        return powerups[ Mathf.FloorToInt(Random.Range(0f, powerups.Count)) ];
    }
}
