using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Animation Settings")]
    public string movementParameter = "IsMoving";
    public string shootTrigger = "Shoot";
    public string hitTrigger = "Hit";
    public string deathTrigger = "Die";

    private bool isDead = false;

    void Start()
    {

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (animator == null)
        {
            Debug.LogWarning("AnimationController: No Animator found!");
        }
        else
        {
            Debug.Log("AnimationController Ready");
        }
    }

    public void SetMovement(bool isMoving)
    {
        if (animator == null || isDead)
            return;

        animator.SetBool(movementParameter, isMoving);
    }

    public void PlayShoot()
    {
        if (animator == null || isDead)
            return;

        animator.SetTrigger(shootTrigger);

        Debug.Log("Shoot Animation Played");
    }

    public void PlayHit()
    {
        if (animator == null || isDead)
            return;

        animator.SetTrigger(hitTrigger);

        Debug.Log("Hit Animation Played");
    }

    public void PlayDeath()
    {
        if (animator == null || isDead)
            return;

        isDead = true;

        animator.SetTrigger(deathTrigger);

        Debug.Log("Death Animation Played");
    }

    public void ResetAnimations()
    {
        isDead = false;

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }
}

