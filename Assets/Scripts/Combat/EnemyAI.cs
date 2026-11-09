using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 25f;
    public int health = 3;

    [Header("Contact Damage")]
    public int contactDamage = 1;
    public float damageCooldown = 1f;

    private Transform player;
    private GameManager gameManager;
    private float nextDamageTime = 0f;
    private bool isDestroyed = false;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: Player not found!");
        }

        gameManager =
            FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (player == null || isDestroyed)
            return;

        if (gameManager != null && gameManager.gameOver)
            return;

        float distance = Vector3.Distance(
            transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector3 direction =
                (player.position - transform.position).normalized;

            transform.position += direction
                * moveSpeed * Time.deltaTime;

            transform.LookAt(player);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed || damage <= 0)
            return;

        health = Mathf.Max(0, health - damage);

        if (gameManager != null)
        {
            gameManager.ShowFeedback("Enemy hit!");
        }

        Debug.Log("Enemy took damage. Health remaining: " + health);

        if (health <= 0)
        {
            isDestroyed = true;

            if (gameManager != null)
            {
                gameManager.ShowFeedback("Enemy destroyed!");
            }

            Debug.Log("Enemy Destroyed");

            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isDestroyed)
            return;

        if (gameManager != null && gameManager.gameOver)
            return;

        if (other.CompareTag("Player") &&
            Time.time >= nextDamageTime)
        {
            PlayerHealth playerHealth =
                other.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);

                nextDamageTime =
                    Time.time + damageCooldown;

                Debug.Log("Enemy damaged player!");
            }
        }
    }
}

