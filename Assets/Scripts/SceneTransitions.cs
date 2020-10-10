using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitions : MonoBehaviour {
    // External References
    public Animator deathTransition;
    public Text CS_text;                // Reference to the Current Score Text
    public Text HS_text;                // Reference to the Highscore Text

    
    // Starts the GoToMenu Transition
    public void GoToMenu() {
        StartCoroutine(PlayGoToMenu());
    }

    // Shows Game Over Canvas
    public void ShowGameOver(int score) {
        // Load in Highscore Data
        SaveSystem.LoadHighScore();
        
        // Get HighScore Data & Save
        HighscoreData hs = HighscoreData.getInstance();
        bool newHS = hs.setNewScore(score);
        SaveSystem.SaveHighScore();

        // Set the Score Texts
        CS_text.text = $"Score: {score}";
        HS_text.text = (newHS ? "NEW " : "") + $"Highscore: {hs.score}";

        // Start the Animation
        deathTransition.SetTrigger("Start");
    }

    // Starts Animation to transition to Main Menu
    public IEnumerator PlayGoToMenu() {
        deathTransition.SetTrigger("GoToMenu");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
