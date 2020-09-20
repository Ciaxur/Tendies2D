using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // External Reference Resources
    public GameObject player;
    public PlatformHandler platformHandler;

    // Environment Boundaries
    public float  leftBoundaryX = -10.0f;
    public float  rightBoundaryX = 10.0f;

    // Infinite Spawning Resources
    public float  dyOffScreen = -5.0f;      // Distance Y Relative to Player that is Offscreen
    public float  dyLookAhead = 20.0f;      // Distance Y Relative to Player to look Ahead to Pre-Spawn/Handle

    // Internal Player References
    private Rigidbody2D playerRbody2D;
    public GameObject debugMenu;


    /**
     * Random Location from given Position
     * @param pos Relative Position
     */
    public Vector2 getRandomLocationFrom(Vector2 pos) {
        int xDirection = Random.Range(0.0f, 1.0f) > 0.5 ? 1 : -1;

        // Constrain X within Boundaries
        float x = Mathf.Clamp(
            pos.x + (xDirection * Random.Range(0.0f, platformHandler.maxDist.x - platformHandler.minDist.x)),
            leftBoundaryX, rightBoundaryX
        );
        float y = (pos.y + platformHandler.minDist.y) + Random.Range(0.0f, platformHandler.maxDist.y - platformHandler.minDist.y);
        return new Vector2(x, y);
    }


    // Physics Update
    void FixedUpdate() {
        // Platform Collision based on Player Movement
        if (this.playerRbody2D.velocity.y < 0) {
            platformHandler.setPlatformCollision(true, this.playerRbody2D.GetComponent<Collider2D>());
        } 
        else if (this.playerRbody2D.velocity.y > 0) {
            platformHandler.setPlatformCollision(false, this.playerRbody2D.GetComponent<Collider2D>());
        }
    }

    // Last Minute Checks
    void LateUpdate() {
        // Despwan Off-Screen Objects
        Vector2 distFirst = platformHandler.getFistPlatform().transform.position - player.transform.position;
        if (distFirst.y < dyOffScreen)
            platformHandler.destroyLast();

        // Handle Spawning Ahead
        Vector2 distLast = platformHandler.getLastPlatform().transform.position - player.transform.position;
        if (distLast.y < dyLookAhead)
            platformHandler.runPlatformSpawns();

        // DEBUG: Information
        debugMenu.GetComponent<TMPro.TextMeshProUGUI>().text = 
            $"Last Platform: {distFirst.y:N2}\n" +
            $"First Platform: {distLast.y:N2}\n" +
            $"Total Platforms: {platformHandler.getTotalPlatforms()}";
    }

    // Start is called before the first frame update
    void Start() {
        // Spawn Initial 10 Platforms
        for (int i = 0; i < 10; i++)
            platformHandler.runPlatformSpawns();

        // Assign Player References
        this.playerRbody2D = this.player.GetComponent<Rigidbody2D>();
    }
}
