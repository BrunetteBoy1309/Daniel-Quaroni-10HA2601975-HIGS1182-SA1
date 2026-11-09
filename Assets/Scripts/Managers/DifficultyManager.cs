using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Difficulty Settings")]
    public float timeBetweenWaves = 20f;
    public float speedIncrease = 0.5f;
    public float maximumEnemySpeed = 8f;

    private float timer = 0f;
    private int wave = 1;

    private GameManager gameManager;

    void Start()
    {
        gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.ShowFeedback("Wave 1 - Survive!");
        }

        Debug.Log("Difficulty Manager Started");
    }

    void Update()
    {
        if (gameManager != null && gameManager.gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= timeBetweenWaves)
        {
            timer = 0f;
            wave++;

            IncreaseDifficulty();
        }
    }

    void IncreaseDifficulty()
    {
        EnemyAI[] enemies =
            FindObjectsByType<EnemyAI>(
                FindObjectsSortMode.None);

        foreach (EnemyAI enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.moveSpeed = Mathf.Min(
                    enemy.moveSpeed + speedIncrease,
                    maximumEnemySpeed);
            }
        }

        if (gameManager != null)
        {
            gameManager.ShowFeedback(
                "Wave " + wave +
                " - Enemies are getting faster!");
        }

        Debug.Log("Difficulty increased to Wave " + wave);
    }

    public int GetCurrentWave()
    {
        return wave;
    }
}
