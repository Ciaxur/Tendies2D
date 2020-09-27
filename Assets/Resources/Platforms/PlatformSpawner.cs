using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Public References
    public GameObject spawnPoint;
    public GameObject enemy;
    public GameObject powerup;


    // Private Data
    public enum SpawnType { ENEMY, POWER_UP };


    // Issues to Spawn on to of Platform
    public void spawn(SpawnType type) {
        switch (type) {
            case SpawnType.ENEMY:
                Debug.Log("Spawning Enemy!");
                Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
                break;
            case SpawnType.POWER_UP:
                Debug.Log("Spawning Power Up!");
                Instantiate(powerup, spawnPoint.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
    
    
}
