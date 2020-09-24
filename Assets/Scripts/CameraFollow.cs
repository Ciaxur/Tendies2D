using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    // External References
    public Transform followObj;                 // Object to Follow
    public Vector3 offset;                    // Camera Position Offset
    public float smoothSpeed = 0.125f;      // The "lag" of the Camera Movement

    // Late Physics Update
    void LateUpdate() {
        // Calculate the Desired Position of the Camera
        Vector3 desiredPosition = offset + new Vector3(
            transform.position.x,
            this.followObj.position.y,
            transform.position.z
        );

        // Add the smoothness to the Camera
        Vector3 newPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.fixedDeltaTime
        );

        // Only Move Upwards
        transform.position = newPosition.y > transform.position.y
            ? newPosition : transform.position;
    }
}
