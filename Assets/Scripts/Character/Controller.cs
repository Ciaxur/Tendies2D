using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // External Settings
    public float movementSpeed = 4.0f;
    public float maxVelocity = 5.0f;

    // Internal References
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = this.gameObject.GetComponent<Rigidbody2D>();
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
    }
}
