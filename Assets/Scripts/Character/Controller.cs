using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    // External Settings
    public float movementSpeed = 4.0f;
    public float maxVelocity = 10.0f;
    public float jumpForceMultiplier = 1.0f;

    // Audio References
    public AudioSource audioSource;
    public AudioClip jumpSound0;
    public AudioClip jumpSound1;
    public AudioClip jumpSound2;

    // Layer Identification
    public int   charLayer;
    public int   floorLayer;

    // Internal References
    Rigidbody2D rbody2D;
    CharacterStatus stats;


    // Internal Key Presses
    private float   inputMoveX      = 0.0f;
    private bool    inputJump       = false;
    private bool    vertKeyDown     = false;

    // Internal States
    private bool    facingRight     = true;     // Starts off Facing Right
    private bool    fallCheck       = false;    // If Player is on Ground


    void playJumpSound() {
        float val = Random.Range(0f, 1f);
        AudioClip audio = jumpSound0;
        
        // Choose from 3 Random Doinks! (BY Dana Naidas)
        if (val < 0.33) {
            audio = jumpSound1;
        } else if (val < 0.66) {
            audio = jumpSound2;
        }

        // Play Sound
        audioSource.PlayOneShot(audio);
    }


    void Awake() {
        rbody2D = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStatus>();
        charLayer = LayerMask.NameToLayer("Character");
        floorLayer = LayerMask.NameToLayer("Floor");
    }


    // Physics Update
    void FixedUpdate() {
        // Update Player Movement
        Vector2 pVel = rbody2D.velocity;
        Vector2 playerForce = new Vector2(inputMoveX, 0.0f);
        if (inputJump && pVel.y == 0) {
            playJumpSound();            
            playerForce.y = jumpForceMultiplier * 1000.0f;
        }
        rbody2D.AddForce(playerForce);

        inputJump = false;

        // Limit Velocity
        float velBoost = Mathf.Clamp(stats.getSpeedBuff() * 0.8f, 1f, 1f + stats.getSpeedBuff());
        pVel.x = Mathf.Clamp(pVel.x, -maxVelocity * stats.getSpeedBuff(), maxVelocity * velBoost);
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
        inputMoveX = Input.GetAxisRaw("Horizontal") * movementSpeed * stats.getSpeedBuff();
        
        // Update Vertical Movement
        if (!vertKeyDown && (Input.GetAxisRaw("Vertical") > 0 || Input.GetKeyDown(KeyCode.Space))) {
            Physics2D.IgnoreLayerCollision(charLayer, floorLayer);
            inputJump = true;
            vertKeyDown = true;
            fallCheck = false;
        }
        else if (Input.GetAxisRaw("Vertical") == 0 || Input.GetKeyDown(KeyCode.Space) == false) {
            vertKeyDown = false;
        }
    }
}
