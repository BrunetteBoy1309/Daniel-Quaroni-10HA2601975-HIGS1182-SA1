using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;
    public BoxCollider spawnArea;
    public int asteroidCount = 10;

    void Start()
    {
        SpawnAsteroids();
    }

    public void SpawnAsteroids()
    {
        if (asteroidPrefab == null)
        {
            Debug.LogError("Asteroid prefab is not assigned!");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("Asteroid spawn area is not assigned!");
            return;
        }

        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();

            Instantiate(
                asteroidPrefab,
                spawnPosition,
                Random.rotation
            );

            Debug.Log("Asteroid Spawned");
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
