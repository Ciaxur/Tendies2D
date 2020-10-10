using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Class
[System.Serializable]
public class HighscoreData {
    // Keep track of Singleton Use
    static HighscoreData theOne;

    // External Data
    public int score;


    // Obtains the ONLY Reference to the Object
    public static HighscoreData getInstance(int score = 0) {
        // The only ONE!
        if (theOne != null) {
            return theOne;
        }
        
        // Create a new Instance and return it
        theOne = new HighscoreData(score);
        return theOne;
    }

    // Private Constructor
    private HighscoreData(int score) {
        this.score = score;
    }


    /**
     * Attempts to set a new score if score is higher
     * @param newScore The new score to try and set
     * @returns True if set, false if not set
     */
    public bool setNewScore(int newScore) {
        if (newScore > score) {
            score = newScore;
            return true;
        }
        return false;
    }
}
