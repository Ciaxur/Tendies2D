using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // External Bullet Stats
    public int damage = 1;
    
    // Internal Settings
    private int characterLayer;
    
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
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        int collidedWith = collision.gameObject.layer;
        
        // Character Hit!
        if( collidedWith == characterLayer ) {
            CharacterStatus otherStats = collision.gameObject.GetComponent<CharacterStatus>();
            if (!otherStats) {
                Debug.Log($"Collided with Null!{collision.gameObject.name}");
                Debug.Break();
            }
            otherStats.inflictDamage(damage);
            Destroy(gameObject);
        }
    }
}
