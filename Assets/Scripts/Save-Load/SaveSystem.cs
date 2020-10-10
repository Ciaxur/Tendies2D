using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    // Paths
    static string highScorePath = $"{Application.persistentDataPath}/highscore.bin";

    public static void SaveHighScore() {
        // Construct Streams & Formatter to Start Saving
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream outStream = new FileStream(highScorePath, FileMode.Create);

        // Get the ONLY Data & Save it
        HighscoreData data = HighscoreData.getInstance();
        formatter.Serialize(outStream, data);
        outStream.Close();
    }

    // Loads Data into Singleton Class
    // Returns True/False to indicate Loaded or not
    public static bool LoadHighScore() {
        // Verify File Exists
        if (File.Exists(highScorePath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream inStream = new FileStream(highScorePath, FileMode.Open);

            // Load in data
            HighscoreData data = formatter.Deserialize(inStream) as HighscoreData;

            // Save the Score
            HighscoreData.getInstance(data.score);
            inStream.Close();

            Debug.Log("Saved File to " + highScorePath);
            return true;
        } else {
            Debug.LogError("Save file not found in " + highScorePath);
            return false;
        }

    }
}
