using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {
    // Main Sprites
    public Sprite defaultSprite;
    public Sprite vibeModeSprite;

    // Optional Material
    public Material vibeMaterial;

    // Internal State
    SpriteRenderer spriteRenderer;
    Material defaultMaterial;


    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }
    

    public void setDefault() {
        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.material = defaultMaterial;
    }

    public void setVibeMode() {
        spriteRenderer.sprite = vibeModeSprite;

        // Check Optional Material
        if (vibeMaterial) {
            spriteRenderer.material = vibeMaterial;
        }
    }
}
