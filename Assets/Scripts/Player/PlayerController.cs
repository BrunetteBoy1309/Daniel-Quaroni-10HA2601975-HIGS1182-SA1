using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;

    [Header("Animation")]
    public AnimationController animationController;

    private bool canMove = true;

    void Start()
    {

        if (animationController == null)
        {
            animationController =
                GetComponent<AnimationController>();
        }

        if (animationController == null)
        {
            Debug.LogWarning(
                "PlayerController: AnimationController not found!");
        }
    }

    void Update()
    {
        if (!canMove)
            return;

        HandleInput();
        HandleRotation();
    }

    void HandleInput()
    {
        Vector3 movement = Vector3.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                movement += transform.forward;

            if (Keyboard.current.sKey.isPressed)
                movement -= transform.forward;

            if (Keyboard.current.aKey.isPressed)
                movement -= transform.right;

            if (Keyboard.current.dKey.isPressed)
                movement += transform.right;

            if (Keyboard.current.spaceKey.isPressed)
                movement += Vector3.up;

            if (Keyboard.current.leftCtrlKey.isPressed)
                movement -= Vector3.up;
        }

        if (movement != Vector3.zero)
        {
            transform.position +=
                movement.normalized *
                moveSpeed *
                Time.deltaTime;
        }


        if (animationController != null)
        {
            animationController.SetMovement(
                movement != Vector3.zero);
        }
    }

    void HandleRotation()
    {
        if (Mouse.current == null)
            return;

        float mouseX =
            Mouse.current.delta.x.ReadValue();

        float mouseY =
            Mouse.current.delta.y.ReadValue();

        transform.Rotate(
            -mouseY * mouseSensitivity * Time.deltaTime,
            mouseX * mouseSensitivity * Time.deltaTime,
            0f
        );
    }

    public void DisableMovement()
    {
        canMove = false;

        if (animationController != null)
        {
            animationController.SetMovement(false);
        }

        Debug.Log("Player movement disabled");
    }
}


