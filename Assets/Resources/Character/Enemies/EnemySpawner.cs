using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Public References to all Available Enemies
    public GameObject skullyPrefab;
    public GameObject slimyPrefab;
    public GameObject starPrefab;
    public GameObject zombiPrefab;

    // Internal State
    List<GameObject> enemies = new List<GameObject>();


    void Awake() {
        enemies = new List<GameObject>{
            skullyPrefab,
            slimyPrefab,
            starPrefab,
            zombiPrefab
        };
    }

    public GameObject getEnemy() {
        return enemies[ Mathf.FloorToInt(Random.Range(0f, enemies.Count)) ];
    }
}
