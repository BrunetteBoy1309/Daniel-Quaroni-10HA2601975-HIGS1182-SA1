using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid Movement")]
    public float minSpeed = 1f;
    public float maxSpeed = 4f;

    [Header("Asteroid Damage")]
    public int contactDamage = 1;

    private Rigidbody rb;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager =
            FindFirstObjectByType<GameManager>();

        if (rb != null)
        {
            Vector3 randomDirection = Random.onUnitSphere;

            float randomSpeed =
                Random.Range(minSpeed, maxSpeed);

            rb.linearVelocity =
                randomDirection * randomSpeed;

            rb.angularVelocity =
                Random.insideUnitSphere * 2f;
        }
        else
        {
            Debug.LogWarning(
                "Asteroid is missing a Rigidbody!");
        }

        Debug.Log("Asteroid Spawned");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager != null && gameManager.gameOver)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                collision.gameObject.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);

                Debug.Log("Asteroid damaged player!");
            }
            else
            {
                Debug.LogWarning(
                    "PlayerHealth component not found!");
            }
        }
    }
}
