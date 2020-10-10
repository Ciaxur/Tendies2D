using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // External References
    public Animator creditsAnimation;

    public void Play() {
        // Go to Game Scene
        Debug.Log("Activating Game Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator PlayCredits() {
        creditsAnimation.SetTrigger("Run");
        yield return new WaitForSeconds(5f);
    }

    // Plays Credits then Exits
    public void PlayExit() {
        StartCoroutine(PlayCredits());
    }
    
    // Actually Quits the Game
    public void Exit() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
