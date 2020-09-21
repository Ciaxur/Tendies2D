using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEmitter : MonoBehaviour
{
    // Various Types of Bubbles
    public GameObject bubbleObj;
    public Sprite bubble1;
    public Sprite bubble2;
    public Sprite bubble3;
    public Sprite bubble4;
    public Sprite bubble5;

    // Bubble Properties
    public GameObject relativeTo;           // Bubble Emitter Relative to
    public float minSpeed = 50.0f;
    public float maxSpeed = 250.0f;
    public float leftBoundary = -10.0f;
    public float rightBoundary = 10.0f;

    // Organized Data
    private List<Sprite> bubbles = new List<Sprite>();

    /**
     * Spawns Bubble Relative to Stored Object
     */
    public void spawn() {
        // Create a Bubble
        GameObject bubble = Instantiate(
            bubbleObj, 
            new Vector3(
                relativeTo.transform.position.x - Random.Range(leftBoundary, rightBoundary), 
                relativeTo.transform.position.y - Random.Range(leftBoundary, rightBoundary)
            ),
            Quaternion.identity);

        // Assign Bubble to a Parent
        bubble.transform.parent = transform;
        
        // Obtain Properties
        Rigidbody2D rBody2D = bubble.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = bubble.GetComponent<SpriteRenderer>();

        // Add Random Speed up & Random Sprite
        rBody2D.AddForce(new Vector2(0.0f, Random.Range(minSpeed, maxSpeed)));
        spriteRenderer.sprite = bubbles[ Mathf.FloorToInt(Random.Range(0.0f, bubbles.Count)) ];

        // Random Alpha
        Color clr = spriteRenderer.color;
        spriteRenderer.color = new Color(clr.r, clr.g, clr.b, Random.Range(0.40f, 0.127f));
    }


    void Start() {
        // Add all Sprites in
        bubbles.Add(bubble1);
        bubbles.Add(bubble2);
        bubbles.Add(bubble3);
        bubbles.Add(bubble4);
        bubbles.Add(bubble5);
    }
}
