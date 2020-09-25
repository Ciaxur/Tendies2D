using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    // External Settings
    public float movementSpeed = 4.0f;

    // External References
    public CharacterController2D controller;

    // Internal Key Presses
    private float inputMoveX = 0.0f;
    private bool inputJump = false;


    // Physics Update
    void FixedUpdate() {
        controller.Move(inputMoveX * Time.fixedDeltaTime, false, inputJump);
        inputJump = false;
    }


    // Update is called once per frame
    void Update() {
        // Update Sideways Movement
        inputMoveX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        // Update Vertical Movement
        if (Input.GetAxisRaw("Vertical") > 0) {
            inputJump = true;
        }
    }
}
