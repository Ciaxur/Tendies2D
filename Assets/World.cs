using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // External Resources
    public GameObject platformPrefab;
    public GameObject player;

    // Distance in Relation to Player and/or Platform
    public Vector2 maxDist = new Vector2(5.0f, 10.0f);
    public Vector2 minDist = new Vector2(0.0f, 2.0f);

    // Internal Resources
    private Queue<GameObject> platforms = new Queue<GameObject>();
    private bool spaceUp = true;        // State of Space Key
    private bool keyD = true;           // State of D Key


    // Random Location from given Position
    private Vector2 getRandomLocationFrom(Vector2 pos)
    {
        int xDirection = Random.Range(0.0f, 1.0f) > 0.5 ? 1 : -1;
        float x = pos.x + (xDirection * Random.Range(0.0f, this.maxDist.x - this.minDist.x));
        float y = (pos.y + this.minDist.y) + Random.Range(0.0f, this.maxDist.y - this.minDist.y);
        return new Vector2(x, y);
    }

    // Spawns a Platform in respect to given Postition
    private void spawnPlatform(Vector2 pos)
    {
        GameObject obj = Instantiate(platformPrefab, getRandomLocationFrom(pos), Quaternion.identity);
        obj.transform.parent = this.transform;
        this.platforms.Enqueue(obj);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && this.spaceUp)
        {
            spawnPlatform(player.transform.position);
            this.spaceUp = false;
        }
        else if (!Input.GetKeyDown(KeyCode.Space) && !this.spaceUp)
        {
            this.spaceUp = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && this.keyD)
        {
            Destroy(this.platforms.Dequeue());
            this.keyD = false;
        }
        else if (!Input.GetKeyDown(KeyCode.D) && !this.keyD)
        {
            this.keyD = true;
        }
    }
}
