using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    // External References
    public MainMenu mainMenu;
    public Animator animator;
    
    public void Reset() {
        animator.enabled = false;
    }
    
    public void Exit() {
        mainMenu.Exit();
    }
}
