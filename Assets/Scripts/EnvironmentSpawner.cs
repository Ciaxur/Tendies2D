using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour {
    // External References
    public GameObject relativeObject;           // Object Relative Position
    public GameObject fishPrefab;               // Fish Prefab
    
    // Spawn Settings
    [Range(0.0f, 50.0f)]  public float x_SpawnOffset   = 10.0f;
    [Range(0.0f, 50.0f)]  public float y_SpawnOffset   = 10.0f;
    [Range(0.0f, 50.0f)]  public float despawnRange    = 10.0f;

    // Entity Velocity
    [Range(50.0f, 500.0f)] public float minVelocity = 100.0f;
    [Range(50.0f, 500.0f)] public float maxVelocity = 200.0f;



    /**
     * Spawns an Entity Relative to given Position
     * @param relPos - Relative Position
     */
    public void spawn(Vector2 relPos) {
        // Calculate Spawn Config
        Vector2 position = new Vector2(
            relPos.x + Random.Range(-x_SpawnOffset, x_SpawnOffset),
            relPos.y + Random.Range(-y_SpawnOffset, y_SpawnOffset)
        );
        int direction = (position.x - relPos.x > 0.0f) ? -1 : 1;
        Vector2 force = new Vector2(
             direction * Random.Range(minVelocity, maxVelocity),
            0.0f
        );
        

        // Create & Assign Entity
        GameObject entity = Instantiate(fishPrefab, position, Quaternion.identity);
        entity.transform.parent = transform;

        // Configure Fish Fish Script
        Fish fish = entity.GetComponent<Fish>();
        fish.setDespawnRange(despawnRange);
        fish.relativeObject = relativeObject;

        // Flip Fish according to Direction
        Vector2 newScale = fish.transform.localScale;
        newScale.x *= -direction;
        fish.transform.localScale = newScale;
        
        Rigidbody2D rbody = entity.GetComponent<Rigidbody2D>();
        rbody.AddForce(force);
    }

}
