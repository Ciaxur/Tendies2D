using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeath : MonoBehaviour {
    // External References
    public float distToDespawn = 100.0f;

    // Internal References
    private Transform player;
    World world;        // Reference to the World


    public void setPlayer(Transform player) {
        this.player = player;
    }

    void Start() {
        if (transform.parent) {
            world = transform.parent.GetComponent<World>();
        }
    }

    /** Keep Track of Changes and Life */
    void LateUpdate() {
        // Get Player Reference
        Transform pos = player;
        if (world) {
            pos = world.getPlayer().transform;
        }
        
        // Check if far enough to Despawn
        float distFromRel = transform.position.y - pos.position.y;
        if (distFromRel >= distToDespawn) {
            Destroy(this.gameObject);
        }
    }
}
