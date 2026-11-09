using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public Projectile raycastShooter;
    public float fireRate = 0.3f;

    [Header("Animation")]
    public AnimationController animationController;

    private float nextFireTime;

    void Start()
    {
        if (animationController == null)
        {
            animationController =
                GetComponentInParent<AnimationController>();
        }
    }

    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed &&
            Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (raycastShooter == null)
            return;

        if (animationController != null)
        {
            animationController.PlayShoot();
        }

        raycastShooter.Shoot();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(
                AudioManager.Instance.laserSound);
        }

        nextFireTime = Time.time + fireRate;
    }
}
