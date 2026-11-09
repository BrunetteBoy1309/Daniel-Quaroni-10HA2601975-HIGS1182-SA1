using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public float shieldDuration = 10f;

    private bool collected = false;

    void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        PlayerHealth health =
            other.GetComponentInParent<PlayerHealth>();

        if (health == null)
            return;

        collected = true;

        health.ActivateShield(shieldDuration);

        GameManager gm =
            FindFirstObjectByType<GameManager>();

        if (gm != null)
        {
            gm.ShowFeedback("ENERGY SHIELD ACTIVATED!");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(
                AudioManager.Instance.scrapSound);
        }

        Debug.Log("Energy Shield Collected!");

        Destroy(gameObject);
    }
}
