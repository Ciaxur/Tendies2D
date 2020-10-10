using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bobbing : MonoBehaviour {
    // External Settings
    public float yOffset = 5f;
    public float smoothness = 1f;

    // Interanal State
    Vector2 initialPosition;
    Vector2 desiredPosition;
    bool goBack = false;


    void Awake() {
        this.initialPosition = transform.position;
        this.desiredPosition = new Vector2(transform.position.x, transform.position.y + yOffset);
    }
    
    void FixedUpdate() {
        Vector2 desired = goBack ? initialPosition : desiredPosition;
        Vector2 newPos = Vector2.Lerp(transform.position, desired, smoothness);
        transform.position = newPos;

        // Check if Destination Reached
        if (Mathf.Abs(desired.y - newPos.y) < 0.2f) {
            goBack = !goBack;
        }
    }
}
