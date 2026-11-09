
using UnityEngine;

public static class SaveManager
{

    private const string HIGH_SCORE_KEY = "HighScore";

    public static int LoadHighScore()
    {
        int savedScore = PlayerPrefs.GetInt(
            HIGH_SCORE_KEY, 0);

        Debug.Log("High Score Loaded: " + savedScore);

        return savedScore;
    }

    public static void SaveHighScore(int score)
    {
        int currentHighScore = LoadHighScore();

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();

            Debug.Log("New High Score Saved: " + score);
        }
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
        PlayerPrefs.Save();

        Debug.Log("High Score Reset");
    }
}
