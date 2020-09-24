using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    // External Settings
    public float movementSpeed = 4.0f;
    public float maxVelocity = 5.0f;
    public float jumpMultiplier = 1.0f;

    // Internal References
    private Rigidbody2D rbody2D;
    private BoxCollider2D boxCollider2D;
    private bool canJump = false;
    private int xDirection = 1;     // Initiated as Right

    // Start is called before the first frame update
    void Start() {
        // Store Attached Components
        this.rbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Reset Jump Ability if Object is a Platform or the Floor
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Floor") {
            this.canJump = true;
        }
    }

    // Physics Update
    void FixedUpdate() {
        // Constrain to Max Velocity
        if (rbody2D.velocity.x > maxVelocity)
            rbody2D.velocity = new Vector2(maxVelocity, rbody2D.velocity.y);
        if (rbody2D.velocity.x < -maxVelocity)
            rbody2D.velocity = new Vector2(-maxVelocity, rbody2D.velocity.y);

        // Flip Character Depending on Direction
        if (xDirection == 1 && rbody2D.velocity.x < -0.5f) {       // Going Left
            this.transform.localScale *= new Vector2(-1.0f, 1.0f);
            xDirection = -1;
        } else if (xDirection == -1 && rbody2D.velocity.x > 0.5f) {       // Going Right
            this.transform.localScale *= new Vector2(-1.0f, 1.0f);
            xDirection = 1;
        }
    }


    // Update is called once per frame
    void Update() {
        // Check Sideways Movement Input
        if (Input.GetKey(KeyCode.RightArrow)) {
            rbody2D.AddForce(new Vector2(1.0f * this.movementSpeed, 0.0f));
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            rbody2D.AddForce(new Vector2(-1.0f * this.movementSpeed, 0.0f));
        }

        // Jumping
        if (Input.GetKey(KeyCode.UpArrow) && canJump) {
            rbody2D.AddForce(new Vector2(0.0f, this.jumpMultiplier * 1000.0f));
            canJump = false;
        }
    }
}
