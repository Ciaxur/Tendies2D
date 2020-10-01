using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    // External References
    public GameObject followRef;
    public float followSmoothness = 0.5f;

    void Start() {
        // Detach from Parent
        transform.SetParent(null);
        transform.position = followRef.transform.position;
    }

    void LateUpdate() {
        // Check if Ref is still there
        if (!followRef) {
            Destroy(gameObject);
        }

        else {
            Vector2 desiredPos = Vector2.Lerp(
                transform.position, 
                followRef.transform.position, 
                followSmoothness
            );

            transform.position = desiredPos;
        }
    }
}
