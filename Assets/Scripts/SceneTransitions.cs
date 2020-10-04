using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour {
    // External References
    public Animator deathTransition;

    
    // Starts the GoToMenu Transition
    public void GoToMenu() {
        StartCoroutine(PlayGoToMenu());
    }

    // Shows Game Over Canvas
    public void ShowGameOver() {
        deathTransition.SetTrigger("Start");
    }

    // Starts Animation to transition to Main Menu
    public IEnumerator PlayGoToMenu() {
        deathTransition.SetTrigger("GoToMenu");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
