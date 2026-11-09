using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera playerCamera;
    public float shootRange = 100f;
    public int damage = 1;

    public void Shoot()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera is not assigned!");
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, shootRange))
        {
            GameManager gm =
                FindFirstObjectByType<GameManager>();

            if (hit.collider.CompareTag("Asteroid"))
            {
                Asteroid asteroid =
                    hit.collider.GetComponentInParent<Asteroid>();

                if (asteroid != null)
                {
                    Destroy(asteroid.gameObject);
                }
                else
                {
                    Destroy(hit.collider.gameObject);
                }

                if (gm != null)
                {
                    gm.ShowFeedback("Asteroid destroyed!");
                }

                Debug.Log("Asteroid destroyed by Raycast!");
                return;
            }

            EnemyAI enemy =
                hit.collider.GetComponentInParent<EnemyAI>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                if (gm != null)
                {
                    gm.ShowFeedback("Enemy hit!");
                }

                Debug.Log("Enemy hit by Raycast!");
                return;
            }

            if (gm != null)
            {
                gm.ShowFeedback("Laser fired!");
            }
        }
        else
        {
            Debug.Log("Laser missed!");
        }
    }
}
