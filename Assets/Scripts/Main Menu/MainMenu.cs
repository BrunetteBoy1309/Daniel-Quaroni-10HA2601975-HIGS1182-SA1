using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text highScoreText;

    void Start()
    {

        int highScore = SaveManager.LoadHighScore();

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }

        Debug.Log("Main Menu Loaded");
    }

    public void StartGame()
    {
        Debug.Log("Starting Gameplay");

        Time.timeScale = 1f;

        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
