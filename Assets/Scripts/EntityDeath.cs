using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeath : MonoBehaviour {
    // External References
    public float distToDespawn = 100.0f;

    // Internal References
    private GameObject refObj;


    public void setRefObject(GameObject refObj) {
        this.refObj = refObj;
    }

    void Start() {
        refObj = GameObject.FindGameObjectWithTag("Player");
    }

    /** Keep Track of Changes and Life */
    void LateUpdate() {
        // Make sure Ref is still there
        if (!refObj) {
            Destroy(gameObject);
        }

        else {
            // Check if far enough to Despawn
            float distFromRel = Vector2.Distance(transform.position, refObj.transform.position);
            if (distFromRel >= distToDespawn) {
                Destroy(this.gameObject);
            }
        }
        
    }
}
