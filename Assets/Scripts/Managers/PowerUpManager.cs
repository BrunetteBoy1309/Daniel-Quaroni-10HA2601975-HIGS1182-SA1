using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public GameObject powerUpPrefab;
    public BoxCollider spawnArea;
    public int powerUpCount = 1;

    void Start()
    {
        SpawnPowerUps();
    }

    public void SpawnPowerUps()
    {
        if (powerUpPrefab == null)
        {
            Debug.LogError("Power-up prefab is not assigned!");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("Power-up spawn area is not assigned!");
            return;
        }

        for (int i = 0; i < powerUpCount; i++)
        {
            Vector3 position = GetRandomPosition();

            Instantiate(
                powerUpPrefab,
                position,
                Quaternion.identity
            );

            Debug.Log("Power-Up Spawned");
        }
    }

    Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
