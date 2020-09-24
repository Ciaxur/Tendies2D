using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {
    // External References
    public float distToDespawn = 100.0f;

    // Internal References
    World world;        // Reference to the World

    void Start() {
        world = transform.parent.GetComponent<World>();
    }

    /** Keep Track of Changes and Life */
    void LateUpdate() {
        // Check if far enough to Despawn
        float distFromRel = transform.position.y - world.getPlayer().transform.position.y;
        if (distFromRel >= distToDespawn) {
            Destroy(this.gameObject);
        }
    }
}
