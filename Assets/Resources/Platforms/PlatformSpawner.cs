using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Public References
    public GameObject spawnPoint;
    public EnemySpawner enemySpawner;
    public PowerUpSpawner powerUpSpawner;

    // Private Data
    public enum SpawnType { ENEMY, POWER_UP };
    

    // Issues to Spawn on to of Platform
    public void spawn(SpawnType type) {
        switch (type) {
            case SpawnType.ENEMY:
                GameObject enemy = enemySpawner.getEnemy();
                enemy = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
                enemy.transform.parent = transform;
                break;
                
            case SpawnType.POWER_UP:
                GameObject powerup = powerUpSpawner.getPowerUp();
                powerup = Instantiate(powerup, spawnPoint.transform.position, Quaternion.identity);
                powerup.transform.parent = transform;
                break;
            default:
                break;
        }
    }
    
    
}
