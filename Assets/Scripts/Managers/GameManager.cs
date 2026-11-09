using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    public int score = 0;
    public int totalScrap = 0;
    public int highScore = 0;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text feedbackText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [Header("Game State")]
    public bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1f;

        highScore = SaveManager.LoadHighScore();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateScore();
        UpdateHighScore();

        ShowFeedback("Collect scrap and avoid enemies!");

        Debug.Log("Game Started");
    }

    void Update()
    {

        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame &&
            !gameOver)
        {
            ReturnToMenu();
        }
    }

    public void SetTotalScrap(int amount)
    {
        totalScrap = amount;
        UpdateScore();

        Debug.Log("Total Scrap Required: " + totalScrap);
    }

    public void CollectScrap(int amount)
    {
        if (gameOver)
            return;

        score += amount;

        if (score > highScore)
        {
            highScore = score;
            SaveManager.SaveHighScore(score);

            Debug.Log("New High Score Saved");
        }

        UpdateScore();
        UpdateHighScore();

        ShowFeedback("Scrap collected! +" + amount);

        Debug.Log("Scrap Collected: " + amount);

        if (score >= totalScrap && totalScrap > 0)
        {
            WinGame();
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Scrap: " + score + " / " + totalScrap;
        }
    }

    void UpdateHighScore()
    {
        if (highScoreText != null)
        {
            highScoreText.text =
                "High Score: " + highScore;
        }
    }

    public void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
        }

        Debug.Log(message);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("Game Over");

        SaveManager.SaveHighScore(score);
        highScore = Mathf.Max(highScore, score);

        ShowFeedback(
            "GAME OVER! Scrap collected: " + score);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(
                AudioManager.Instance.gameOverSound);
        }

        PlayerController player =
            FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.DisableMovement();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        UpdateHighScore();

        StartCoroutine(PauseAfterGameOverSound());
    }

    IEnumerator PauseAfterGameOverSound()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 0f;
    }

    void WinGame()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("Player Won the Game!");

        SaveManager.SaveHighScore(score);
        highScore = Mathf.Max(highScore, score);

        ShowFeedback("YOU WIN! All scrap collected!");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        UpdateHighScore();

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        Debug.Log("Restarting Game");

        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        Debug.Log("Returning to Main Menu");

        SceneManager.LoadScene("Main Menu");
    }
}
