using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    // External Reference Resources
    public GameObject player;
    public PlatformHandler platformHandler;
    public BubbleEmitter bubbleParticles;
    public float bubbleSpawnRate = 0.05f;           // Defaulted to 5%
    public GameObject mainCamera;
    public float playerDistTillDeath = -5.0f;       // Distance from Camera (Off-Screen)
    public GameObject debugMenu;

    // External Settings
    public float distLevel0 = 0.0f;           // Depth Levels
    public float distLevel1 = 100.0f;
    public float distLevel2 = 800.0f;

    // Water Background
    public SpriteRenderer waterBackground;
    public Color startingColor = new Color(22.0f, 22.0f, 22.0f, 255.0f);
    public Color finalColor = new Color(255.0f, 255.0f, 255.0f, 255.0f);
    public float colorChangeDt = 0.1f;

    // Floor & Resources
    public GameObject environmentFloor;
    private float score;

    // Environment Boundaries
    public float leftBoundaryX = -10.0f;
    public float rightBoundaryX = 10.0f;

    // Infinite Spawning Resources
    public float dyOffScreen = -5.0f;      // Distance Y Relative to Player that is Offscreen
    public float dyLookAhead = 20.0f;      // Distance Y Relative to Player to look Ahead to Pre-Spawn/Handle

    // Internal References
    private Rigidbody2D playerRbody2D;
    private EnvironmentSpawner envSpawner;
    private Vector2 distFromFloor = new Vector2(0.0f, 0.0f);     // Keep track of Distance from Floor

    // Environment Spawner Settings
    [Range(0.0f, 1.0f)] public float envSpawnRate       = 0.025f;



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

    // Returns the Distance from Floor
    public Vector2 getDistFromFloor() {
        return distFromFloor;
    }

    public GameObject getPlayer() {
        return this.player;
    }

    // Physics Fixed Updates
    void FixedUpdate() {
        if (Random.Range(0.0f, 1.0f) < envSpawnRate) {
            envSpawner.spawn(player.transform.position);
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
        distFromFloor = player.transform.position - environmentFloor.transform.position;
        score = distFromFloor.y > score ? distFromFloor.y : score;

        // Change Background Color as Player Proceeds
        Color waterClr = waterBackground.color;
        waterBackground.color = Color.Lerp(startingColor, finalColor, colorChangeDt * distFromFloor.y);

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

    void Awake() {
        // Assign References
        playerRbody2D = player.GetComponent<Rigidbody2D>();
        envSpawner = GetComponent<EnvironmentSpawner>();
    }

    // Start is called before the first frame update
    void Start() {
        // Spawn Initial 10 Platforms
        for (int i = 0; i < 10; i++)
            platformHandler.runPlatformSpawns();

        

        // Starting Score
        score = 0.0f;

        // Assign Background Color
        waterBackground.color = startingColor;
    }
}
