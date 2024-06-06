using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public GameObject applePrefab; // Prefab for the rare collectible (apple)
    public Transform cameraTransform;
    public int numberOfStartingTiles = 6;
    public float tileLength = 3f;
    public float initialSpawnRate = 2f;
    public float spawnRateIncreaseRate = 0.01f;
    public int coinTrailLength = 5;
    public float coinSpawnChance = 0.4f;
    public float appleSpawnChance = 0.05f;

    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> activeObstacles = new List<GameObject>();
    private List<GameObject> activeCoins = new List<GameObject>();
    private List<GameObject> activeApples = new List<GameObject>();
    private float spawnZ = 0.0f;
    private int tilesToDelete = 3;
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(IncreaseSpawnRateOverTime());

        // Spawn the initial tiles without obstacles
        for (int i = 0; i < numberOfStartingTiles; i++)
        {
            SpawnTile(i >= 5); // Start spawning obstacles on the 6th tile
        }
    }

    void Update()
    {
        // Check if a new tile needs to be spawned
        if (cameraTransform.position.z > (spawnZ - numberOfStartingTiles * tileLength))
        {
            SpawnTile(true);

            // Delete old tiles and obstacles to save performance
            if (activeTiles.Count > numberOfStartingTiles + tilesToDelete)
            {
                DeleteOldTile();
            }
        }
    }

    void SpawnTile(bool spawnObstacles)
    {
        GameObject tile = Instantiate(tilePrefabs[RandomPrefabIndex()], new Vector3(0, 0, spawnZ), Quaternion.identity);
        tile.transform.SetParent(transform);
        activeTiles.Add(tile);

        if (spawnObstacles)
        {
            SpawnObstacle(spawnZ, tile.transform.position.y, tile);
            if (Random.value < coinSpawnChance)
            {
                SpawnCoinTrail(spawnZ, tile.transform.position.y, tile);
            }
            if (Random.value < appleSpawnChance)
            {
                SpawnApple(spawnZ, tile.transform.position.y, tile);
            }
        }

        spawnZ += tileLength;
    }

    void DeleteOldTile()
    {
        GameObject oldTile = activeTiles[0];
        activeTiles.RemoveAt(0);

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i] != null && activeObstacles[i].transform.parent == oldTile.transform)
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }

        for (int i = activeCoins.Count - 1; i >= 0; i--)
        {
            if (activeCoins[i] != null && activeCoins[i].transform.parent == oldTile.transform)
            {
                Destroy(activeCoins[i]);
                activeCoins.RemoveAt(i);
            }
        }

        for (int i = activeApples.Count - 1; i >= 0; i--)
        {
            if (activeApples[i] != null && activeApples[i].transform.parent == oldTile.transform)
            {
                Destroy(activeApples[i]);
                activeApples.RemoveAt(i);
            }
        }

        Destroy(oldTile);
    }

    void SpawnObstacle(float zPosition, float tileYPosition, GameObject parentTile)
    {
        int[] lanes = new int[] { -1, 0, 1 };
        List<int> usedLanes = new List<int>();

        int numberOfObstacles = Random.Range(1, 3);
        for (int i = 0; i < numberOfObstacles; i++)
        {
            int laneIndex = Random.Range(0, lanes.Length);
            while (usedLanes.Contains(lanes[laneIndex]))
            {
                laneIndex = Random.Range(0, lanes.Length);
            }
            usedLanes.Add(lanes[laneIndex]);

            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                new Vector3(lanes[laneIndex], tileYPosition + 0.16f, zPosition), Quaternion.identity);
            obstacle.transform.SetParent(parentTile.transform);
            activeObstacles.Add(obstacle);
        }
    }

    void SpawnCoinTrail(float zPosition, float tileYPosition, GameObject parentTile)
    {
        int startLane = Random.Range(-1, 2);
        bool coinTrailFits = true;

        for (int i = 0; i < coinTrailLength; i++)
        {
            float coinZPosition = zPosition + (i * 1f);
            foreach (GameObject obstacle in activeObstacles)
            {
                if (obstacle.transform.position.z == coinZPosition && obstacle.transform.position.x == startLane)
                {
                    coinTrailFits = false;
                    break;
                }
            }
            if (!coinTrailFits)
            {
                break;
            }
        }

        if (coinTrailFits)
        {
            for (int i = 0; i < coinTrailLength; i++)
            {
                float coinZPosition = zPosition + (i * 1f);
                GameObject coin = Instantiate(coinPrefab, new Vector3(startLane, tileYPosition + 0.4f, coinZPosition), Quaternion.Euler(90, 0, 0));
                coin.transform.SetParent(parentTile.transform);
                activeCoins.Add(coin);
            }
        }
    }

    void SpawnApple(float zPosition, float tileYPosition, GameObject parentTile)
    {
        int lane = Random.Range(-1, 2);
        GameObject apple = Instantiate(applePrefab, new Vector3(lane, tileYPosition + 0.4f, zPosition), Quaternion.identity);
        apple.transform.SetParent(parentTile.transform);
        activeApples.Add(apple);
    }

    int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = Random.Range(0, tilePrefabs.Length);
        return randomIndex;
    }

    IEnumerator IncreaseSpawnRateOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            currentSpawnRate += spawnRateIncreaseRate;
        }
    }
}