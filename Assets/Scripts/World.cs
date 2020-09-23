using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // External Reference Resources
    public GameObject player;
    public PlatformHandler platformHandler;
    public BubbleEmitter bubbleParticles;
    public float bubbleSpawnRate = 0.05f;           // Defaulted to 5%
    public GameObject mainCamera;
    public float playerDistTillDeath = -5.0f;       // Distance from Camera (Off-Screen)

    // Water Background
    public SpriteRenderer waterBackground;
    public Color startingColor = new Color(22.0f, 22.0f, 22.0f, 255.0f);
    public Color finalColor = new Color(255.0f, 255.0f, 255.0f, 255.0f);
    public float colorChangeDt = 0.1f;

    // Floor & Resources
    public GameObject environmentFloor;
    private float score;

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

    public GameObject getPlayer() {
        return this.player;
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

        // Calculate Score: Only Store the Highest
        Vector2 distFloor = player.transform.position - environmentFloor.transform.position;
        score = distFloor.y > score ? distFloor.y : score;

        // Change Background Color as Player Proceeds
        Color waterClr = waterBackground.color;
        waterBackground.color = Color.Lerp(startingColor, finalColor, colorChangeDt * distFloor.y);

        // Check if Player is Off-Screen
        // TODO: End Game
        float playerToCameraDist = player.transform.position.y - mainCamera.transform.position.y;
        if (playerToCameraDist <= playerDistTillDeath) {
            Debug.Log("DEATH!");
        }

        // Randomly Spawn Bubbles
        if (Random.Range(0.0f, 1.0f) < bubbleSpawnRate)
            bubbleParticles.spawn();

        // DEBUG: Information
        debugMenu.GetComponent<TMPro.TextMeshProUGUI>().text = 
            $"Last Platform: {distFirst.y:N2}\n" +
            $"First Platform: {distLast.y:N2}\n" +
            $"Total Platforms: {platformHandler.getTotalPlatforms()}\n" +
            $"Score: {score:N0}\n" +
            $"dCamera:Player {playerToCameraDist:N2}\n" +
            $"Velocity.x:Player {playerRbody2D.velocity.x:N2}\n";
    }

    // Start is called before the first frame update
    void Start() {
        // Spawn Initial 10 Platforms
        for (int i = 0; i < 10; i++)
            platformHandler.runPlatformSpawns();

        // Assign Player References
        this.playerRbody2D = this.player.GetComponent<Rigidbody2D>();

        // Starting Score
        score = 0.0f;

        // Assign Background Color
        waterBackground.color = startingColor;
    }
}
