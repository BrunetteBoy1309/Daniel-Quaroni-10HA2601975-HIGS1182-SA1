using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -6f);
    public float followSpeed = 10f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        transform.rotation = target.rotation;
    }
}

