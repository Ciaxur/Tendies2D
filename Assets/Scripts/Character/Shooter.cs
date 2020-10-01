using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // External References
    public GameObject bullet;
    public float bulletSpeed = 10f;

    // Internal States
    Rigidbody2D rb;
    Vector2 mousePos;
    bool fireClicked = false;    


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (fireClicked) {
            // Rotate FirePoint in Direction of Mouse
            Vector2 lookDirection = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            GameObject obj = Instantiate(bullet, rb.position, Quaternion.Euler(0f, 0f, angle));
            obj.GetComponent<EntityDeath>().setPlayer(transform);

            Rigidbody2D bulletRb = obj.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(lookDirection.normalized * bulletSpeed, ForceMode2D.Impulse);
            
            fireClicked = false;
        }
    }

    void Update() {
        // Get Mouse Position in World View
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Mouse State
        if (Input.GetButtonDown("Fire1")) {
            fireClicked = true;
        }
    }
}
