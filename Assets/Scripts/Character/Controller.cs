using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    // External Settings
    public float movementSpeed = 4.0f;
    public float maxVelocity = 10.0f;
    public float jumpForceMultiplier = 1.0f;

    // Layer Identification
    public int   charLayer;
    public int   floorLayer;


    // Internal References
    private Rigidbody2D rbody2D;

    // Internal Key Presses
    private float   inputMoveX      = 0.0f;
    private bool    inputJump       = false;
    private bool    vertKeyDown     = false;

    // Internal States
    private bool    facingRight     = true;     // Starts off Facing Right
    private bool    fallCheck       = false;    // If Player is on Ground


    void Awake() {
        rbody2D = GetComponent<Rigidbody2D>();
        charLayer = LayerMask.NameToLayer("Character");
        floorLayer = LayerMask.NameToLayer("Floor");
    }


    // Physics Update
    void FixedUpdate() {
        // Update Player Movement
        Vector2 pVel = rbody2D.velocity;
        Vector2 playerForce = new Vector2(inputMoveX, 0.0f);
        if (inputJump && pVel.y == 0) {
            playerForce.y = jumpForceMultiplier * 1000.0f;
            inputJump = false;
        }
        rbody2D.AddForce(playerForce);

        // Limit Velocity
        pVel.x = Mathf.Clamp(pVel.x, -maxVelocity, maxVelocity);
        rbody2D.velocity = pVel;

        // Adjust Player Facing Direction
        Vector2 scale = transform.localScale;
        if ((inputMoveX > 0.0f && !facingRight) || (inputMoveX < 0.0f && facingRight)) {   // Flip Character
            facingRight = !facingRight;
            scale.x *= -1.0f;
            transform.localScale = scale;
        }


        // If the Player is Falling, Reactivate Floor Collison
        if (!fallCheck && pVel.y < 0.0f) {
            Physics2D.IgnoreLayerCollision(charLayer, floorLayer, false);
            fallCheck = true;
        }
    }


    // Update is called once per frame
    void Update() {
        // Update Sideways Movement
        inputMoveX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        // Update Vertical Movement
        if (!vertKeyDown && Input.GetAxisRaw("Vertical") > 0) {
            Physics2D.IgnoreLayerCollision(charLayer, floorLayer);
            inputJump = true;
            vertKeyDown = true;
            fallCheck = false;
        }
        else if (Input.GetAxisRaw("Vertical") == 0) {
            vertKeyDown = false;
        }
    }
}
