using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // External Settings
    public float minShootDelay= 2f;     // 2 Game Seconds
    public float maxShootDelay= 5f;
    public bool  facingRight = true;     // Facing Right

    // Internal References
    GameObject player;
    EnemyShooter shooter;

    // Internal State
    float   lastShotTime = 0f;          // Last Time when Shot
    
    
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        shooter = GetComponent<EnemyShooter>();
    }

    void FixedUpdate() {
        // Face the Player
        float dir = player.transform.position.x - transform.position.x;
        Vector2 scale = transform.localScale;
        if ((dir > 0f && !facingRight) || (dir < 0f && facingRight)) {
            scale.x *= -1;
            facingRight = !facingRight;
            transform.localScale = scale;
        }
    }

    void LateUpdate() {
        // Calculate Time since last Shot
        float dTime = Time.time - lastShotTime;

        // Determine to Shoot
        float delayTime = Random.Range(minShootDelay, maxShootDelay);
        if( dTime >= delayTime ) {
            shooter.shootAt(player.transform.position);
            lastShotTime = Time.time;
        }
    }
}
