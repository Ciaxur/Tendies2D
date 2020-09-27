using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour {
    // External Reference Resources
    public GameObject platformPrefab;
    public GameObject player;
    public World world;

    // Distance in Relation to Player and/or Platform
    public Vector2 maxDist = new Vector2(5.0f, 10.0f);
    public Vector2 minDist = new Vector2(0.0f, 2.0f);
    public float platformSpacing = 4.0f;

    // Internal Platform Resources
    private Queue<GameObject> platforms = new Queue<GameObject>();
    private int latestPlatformCount = 4;                                    // Last N-Platforms
    private Queue<GameObject> latestPlatforms = new Queue<GameObject>();    // Keeps track of Last N-Plaforms
    private GameObject lastPlaform;                                         // Keeps track of the Last Spawned Platform

    private int spawnIterationCount = 0;        // DEBUG: Make sure not to go over-board


    /** 
     * Spawns a Platform in respect to given Postition
     * @param pos Relative Position
     */
    private void spawnPlatform(Vector2 pos) {
        spawnIterationCount++;
        // Get Random Location for Platform
        Vector2 platformPosition = world.getRandomLocationFrom(pos);

        if (spawnIterationCount < 100) {
            // Make sure Spacing is Accurate
            foreach (GameObject platform in this.platforms) {
                float dist = Vector2.Distance(platformPosition, platform.transform.position);
                if (dist < this.platformSpacing) {
                    // Try a better Approach
                    spawnPlatform(platform.transform.position);
                    return;
                }
            }
        }
        Debug.Log("Spawn Iteration = " + spawnIterationCount);


        GameObject obj = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        obj.transform.parent = this.transform;
        platforms.Enqueue(obj);

        // Keep track of Latest Platforms
        latestPlatforms.Enqueue(obj);
        if (latestPlatforms.Count > latestPlatformCount)
            latestPlatforms.Dequeue();

        // Keep track of the Last Platform
        lastPlaform = obj;

        // Reset the Iteration Count
        spawnIterationCount = 0;
    }

    /** Runs Platform Spawning Algorithm */
    public void runPlatformSpawns() {
        // Convert last Platforms to Array
        GameObject[] plats = latestPlatforms.ToArray();
        GameObject relObj;

        // Initially include Player
        if (latestPlatforms.Count < latestPlatformCount) {
            // Relative to Player Chance if other Plats are there (25% Chance)
            if (latestPlatforms.Count == 0 || Random.Range(0.0f, 1.0f) < 0.25f) {
                relObj = player;
            } else {
                // Pick & Spawn Relative to Random Platform
                GameObject randPlat = plats[Mathf.FloorToInt(Random.Range(0.0f, plats.Length))];
                relObj = randPlat;
            }
        }

        // Spawn Relative to last N-Platforms
        else {
            relObj = plats[Mathf.FloorToInt(Random.Range(0.0f, plats.Length))];
        }

        // Check for Integrity of Platform
        if (relObj == null) {    // Default to Player if Null
            relObj = player;
        }

        // Spawn to Relative Selected Position
        spawnPlatform(relObj.transform.position);
    }

    /**
     * Removes the last N-Platforms
     * @param numPlats The number of Platforms to remove
     */
    public void destroyLast(int numPlats = 1) {
        for (int i = 0; i < numPlats; i++) {
            if (platforms.Count == 0) // Exit if Empty
                return;
            Destroy(platforms.Dequeue());
        }
    }

    /**
     * Removes all the Platforms
     */
    public void clearPlatforms() {
        foreach (GameObject platform in platforms)
            Destroy(platform);
        platforms.Clear();
        latestPlatforms.Clear();
    }

    /** Returns the Last Platform */
    public GameObject getLastPlatform() {
        return lastPlaform;
    }

    /** Returns the First Platform */
    public GameObject getFistPlatform() {
        return platforms.Peek();
    }

    /** Returns the Total Number of Platforms */
    public int getTotalPlatforms() {
        return platforms.Count;
    }

}
