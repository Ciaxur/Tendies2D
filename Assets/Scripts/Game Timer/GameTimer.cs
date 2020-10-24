using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // Time Types
    public enum TYPE { PROGESS, ENEMY_KILL };
    
    // External References
    public Text timerText;
    public float initialTimer = 5f;
    public bool timerStarted = false;
    public Animator animator;

    // Time Increases
    public float timeOnProgress = 5f;
    public float timeOnEnemyKill = 5f;

    // Internal State
    float timer;
    
    // Sets the Timer Text to a set Value
    void setTimerText(float value) {
        timerText.text = $"Timer: {value:N2}";
    }
    

    // Colorz
    Color green = new Color(0.505f, 0.875f, 0.266f, 1f);
    Color red = new Color(0.914f, 0.266f, 0.266f, 1f);
    Color yellow = new Color(0.890f, 0.890f, 0.266f, 1f);
    Color white = new Color(1f, 1f, 1f, 1f);
    bool isAnimPlay = false;
    
    // Runs animation for exmpanding Timer
    // Changes Color from 1 -> 2
    IEnumerator expandTimer(Color color1, Color color2) {
        isAnimPlay = true;
        
        timerText.color = color1;
        animator.SetTrigger("ExpandTimer");
        yield return new WaitForSeconds(0.4f);
        timerText.color = color2;

        isAnimPlay = false;
    }

    // Public method to Add based on Type to the Timer
    public void addToTime(GameTimer.TYPE timeType) {
        if (!isAnimPlay) {
            Color currentColor = timerText.color;
            StartCoroutine(expandTimer(green, currentColor));
        }

        switch(timeType) {
            case TYPE.ENEMY_KILL:
                timer += timeOnEnemyKill;
                break;
            case TYPE.PROGESS:
                timer += timeOnProgress;
                break;
            default:
                break;
        }
    }
    

    // Set the Time on Start
    void Start() {
        timer = initialTimer;
    }

    // Set the Time Text
    void LateUpdate() {
        setTimerText(timer);

        // Check Timer Ranges & Set Color
        if (!isAnimPlay) {
            // Panic Time!
            if (timer < 5.0f) {
                StartCoroutine(expandTimer(red, red));
            }

            // Warning Time
            else if (timer < 7.5f) {
                timerText.color = yellow;
            }

            // Safe Time
            else {
                timerText.color = white;
            }
        }
    }

    // Update the Time
    void Update() {
        if (timerStarted) {
            timer -= Time.deltaTime;
        }

        // If the Timer hits zero, GAMEOVER!
        if (timer <= 0f) {
            World world = FindObjectOfType<World>();
            world.gameOver();
        }
    }
}
