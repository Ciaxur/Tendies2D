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

    IEnumerator CreditsAnimation() {
        creditsAnimation.SetTrigger("Run");
        yield return new WaitForSeconds(5.5f);
    }

    // Plays Credits
    public void PlayCredits() {
        StartCoroutine(CreditsAnimation());
    }
    
    // Actually Quits the Game
    public void Exit() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
