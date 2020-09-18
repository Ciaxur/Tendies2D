using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // External Resources
    public GameObject platformPrefab;
    public GameObject player;

    // Distance in Relation to Player and/or Platform
    public Vector2  maxDist             = new Vector2(5.0f, 10.0f);
    public Vector2  minDist             = new Vector2(0.0f, 2.0f);
    public float    platformSpacing     = 4.0f;

    // Environment Boundaries
    public float  leftBoundaryX = -10.0f;
    public float  rightBoundaryX = 10.0f;

    // Internal Resources
    private Queue<GameObject> platforms = new Queue<GameObject>();
    private bool spaceUp = true;        // State of Space Key
    private bool keyD = true;           // State of D Key

    // Internal Player References
    private Rigidbody2D playerRbody2D;


    // Random Location from given Position
    private Vector2 getRandomLocationFrom(Vector2 pos)
    {
        int xDirection = Random.Range(0.0f, 1.0f) > 0.5 ? 1 : -1;

        // Constrain X within Boundaries
        float x = Mathf.Clamp(
            pos.x + (xDirection * Random.Range(0.0f, this.maxDist.x - this.minDist.x)),
            leftBoundaryX, rightBoundaryX
        );
        float y = (pos.y + this.minDist.y) + Random.Range(0.0f, this.maxDist.y - this.minDist.y);
        return new Vector2(x, y);
    }

    // Spawns a Platform in respect to given Postition
    public void spawnPlatform(Vector2 pos)
    {
        // Get Random Location for Platform
        Vector2 platformPosition = getRandomLocationFrom(pos);

        // Make sure Spacing is Accurate
        foreach (GameObject platform in this.platforms)
        {
            float dist = Vector2.Distance(platformPosition, platform.transform.position);
            if(dist < this.platformSpacing)
            {
                // Try a better Approach
                spawnPlatform(platform.transform.position);
                return;
            }
        }

        GameObject obj = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        obj.transform.parent = this.transform;
        this.platforms.Enqueue(obj);
    }

    // Helper Function: Sets Platform Collision
    private void setPlatformCollision(bool state) 
    {
        Collider2D playerCollider = this.playerRbody2D.GetComponent<Collider2D>();
        foreach (GameObject platform in this.platforms)
            Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), playerCollider, !state);
    }


    // Physics Update
    void FixedUpdate() 
    {
        // Platform Collision based on Player Movement
        if (this.playerRbody2D.velocity.y < 0) {
            this.setPlatformCollision(true);
        } 
        else if (this.playerRbody2D.velocity.y > 0) {
            this.setPlatformCollision(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Spawn Initial Platforms
        spawnPlatform(player.transform.position);

        // Assign Player References
        this.playerRbody2D = this.player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
