using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [Header("Collectible Settings")]
    public GameObject collectiblePrefab;
    public BoxCollider spawnArea;
    public int collectibleCount = 5;

    void Start()
    {
        SpawnCollectibles();
    }

    public void SpawnCollectibles()
    {
        if (collectiblePrefab == null)
        {
            Debug.LogError("Collectible prefab is not assigned!");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("Collectible spawn area is not assigned!");
            return;
        }

        int spawnedCount = 0;

        for (int i = 0; i < collectibleCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();

            GameObject collectible = Instantiate(
                collectiblePrefab,
                spawnPosition,
                Random.rotation
            );

            if (collectible != null)
            {
                spawnedCount++;
                Debug.Log("Collectible Spawned");
            }
        }

        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.SetTotalScrap(spawnedCount);
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
