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

    // Time Increases
    public float timeOnProgress = 5f;
    public float timeOnEnemyKill = 5f;

    // Internal State
    float timer;
    
    // Sets the Timer Text to a set Value
    void setTimerText(float value) {
        timerText.text = $"Timer: {value:N2}";
    }
    

    // Public method to Add based on Type to the Timer
    public void addToTime(GameTimer.TYPE timeType) {
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
    }

    // Update the Time
    void Update() {
        timer -= Time.deltaTime;

        // If the Timer hits zero, GAMEOVER!
        if (timer <= 0f) {
            World world = FindObjectOfType<World>();
            world.gameOver();
        }
    }
}
