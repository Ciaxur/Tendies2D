using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Play() {
        // Go to Game Scene
        Debug.Log("Activating Game Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Exit() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
