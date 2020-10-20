using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // External Bullet Stats
    public int damage = 1;
    
    // Internal Settings
    private int characterLayer;
    private int enemyLayer;
    
    void Start() {
        // Layers to Ignore
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"), 
            LayerMask.NameToLayer("Floor")
        );
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"), 
            LayerMask.NameToLayer("Boundaries")
        );
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"), 
            LayerMask.NameToLayer("Default")
        );


        // Character Layer to Collide with
        characterLayer = LayerMask.NameToLayer("Character");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        int collidedWith = other.gameObject.layer;
        
        // Character / Enemy Hit!
        if( collidedWith == characterLayer || collidedWith == enemyLayer ) {
            CharacterStatus otherStats = other.gameObject.GetComponent<CharacterStatus>();
            if (!otherStats) {
                Debug.Log($"Collided with Null!{other.gameObject.name}");
                Debug.Break();
            }
            otherStats.inflictDamage(damage);
            Destroy(gameObject);
        }
    }
}
