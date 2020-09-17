using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // External Settings
    public float movementSpeed = 4.0f;
    public float maxVelocity = 5.0f;
    public float jumpMultiplier = 1.0f;

    // Internal References
    private Rigidbody2D rbody2D;
    private bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset Jump Ability if Object is a Platform
        if (collision.gameObject.tag == "Platform")
            canJump = true;
    }


    // Physics Update
    void FixedUpdate()
    {
        // Constrain to Max Velocity
        if (rbody2D.velocity.x > maxVelocity)
            rbody2D.velocity = new Vector2(maxVelocity, rbody2D.velocity.y);
        if (rbody2D.velocity.x < -maxVelocity)
            rbody2D.velocity = new Vector2(-maxVelocity, rbody2D.velocity.y);

    }


    // Update is called once per frame
    void Update()
    {
        // Check Sideways Movement Input
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rbody2D.AddForce(new Vector2(1.0f * this.movementSpeed, 0.0f));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rbody2D.AddForce(new Vector2(-1.0f * this.movementSpeed, 0.0f));
        }

        // Jumping
        if(Input.GetKey(KeyCode.UpArrow) && canJump)
        {
            Debug.Log("Jump!");
            rbody2D.AddForce(new Vector2(0.0f, this.jumpMultiplier * 1000.0f));
            canJump = false;
        }
    }
}
