using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    public int currentHealth = 3;
    public TMP_Text healthText;

    [Header("Shield")]
    public bool shieldActive = false;

    [Header("Animation")]
    public AnimationController animationController;

    void Start()
    {
        currentHealth = maxHealth;

        if (animationController == null)
        {
            animationController =
                GetComponent<AnimationController>();
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (shieldActive || currentHealth <= 0 || damage <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(
                AudioManager.Instance.hitSound);
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            if (animationController != null)
            {
                animationController.PlayDeath();
            }

            GameManager gm =
                FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                gm.GameOver();
            }
        }
        else
        {
            if (animationController != null)
            {
                animationController.PlayHit();
            }
        }
    }

    public void ActivateShield(float duration)
    {
        if (duration <= 0)
            return;

        StopCoroutine(nameof(ShieldTimer));
        StartCoroutine(ShieldTimer(duration));
    }

    IEnumerator ShieldTimer(float duration)
    {
        shieldActive = true;

        yield return new WaitForSeconds(duration);

        shieldActive = false;
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text =
                "Health: " + currentHealth + "/" + maxHealth;
        }
    }
}
