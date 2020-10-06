using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // External References
    public GameObject relativeObject;           // Object Relative Position
    public float      despawnRange = 10.0f;     // Despawn Range Relative to Object


    // Sets the Despawn Range from Relative Object
    public void setDespawnRange(float range) {
        this.despawnRange = range;
    }

    void FixedUpdate() {
        // Early Return if Object is Destroyed
        if (!relativeObject) {
            Destroy(gameObject);
            return;
        }
        
        float rangeFromDespawn = Vector2.Distance(
            transform.position,
            relativeObject.transform.position
        );
        
        if (rangeFromDespawn >= despawnRange) {
            Destroy(this.gameObject);
        }
    }
}
