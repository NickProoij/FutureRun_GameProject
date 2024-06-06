using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public GameObject applePrefab;
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
            // Start spawning obstacles on the 6th tile
            SpawnTile(i >= 5); 
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
        // Instantiate a new tile at the current spawn position
        GameObject tile = Instantiate(tilePrefabs[RandomPrefabIndex()], new Vector3(0, 0, spawnZ), Quaternion.identity);
        tile.transform.SetParent(transform);
        activeTiles.Add(tile);

        // Spawn obstacles and coins on the new tile if required
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

        // Update the spawn position for the next tile
        spawnZ += tileLength;
    }

    void DeleteOldTile()
    {
        // Remove the oldest tile
        GameObject oldTile = activeTiles[0];
        activeTiles.RemoveAt(0);

        // Remove obstacles that are associated with the oldest tile
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i] != null && activeObstacles[i].transform.parent == oldTile.transform)
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }

        // Remove coins that are associated with the oldest tile
        for (int i = activeCoins.Count - 1; i >= 0; i--)
        {
            if (activeCoins[i] != null && activeCoins[i].transform.parent == oldTile.transform)
            {
                Destroy(activeCoins[i]);
                activeCoins.RemoveAt(i);
            }
        }

        // Remove apples that are associated with the oldest tile
        for (int i = activeApples.Count - 1; i >= 0; i--)
        {
            if (activeApples[i] != null && activeApples[i].transform.parent == oldTile.transform)
            {
                Destroy(activeApples[i]);
                activeApples.RemoveAt(i);
            }
        }

        // Destroy the old tile
        Destroy(oldTile);
    }

    void SpawnObstacle(float zPosition, float tileYPosition, GameObject parentTile)
    {
        // Decide lanes for obstacles
        int[] lanes = new int[] { -1, 0, 1 };
        List<int> usedLanes = new List<int>();

        // Ensure not all three lanes are blocked
        int numberOfObstacles = Random.Range(1, 3);
        for (int i = 0; i < numberOfObstacles; i++)
        {
            int laneIndex = Random.Range(0, lanes.Length);
            while (usedLanes.Contains(lanes[laneIndex]))
            {
                laneIndex = Random.Range(0, lanes.Length);
            }
            usedLanes.Add(lanes[laneIndex]);

            // Instantiate obstacle at the chosen lane
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                new Vector3(lanes[laneIndex], tileYPosition + 0.16f, zPosition), Quaternion.identity);
            obstacle.transform.SetParent(parentTile.transform);
            activeObstacles.Add(obstacle);
        }
    }

    void SpawnCoinTrail(float zPosition, float tileYPosition, GameObject parentTile)
    {
        // Determine the starting lane for the coin trail
        int startLane = Random.Range(-1, 2);
        bool coinTrailFits = true;

        // Check if the coin trail fits without overlapping with obstacles
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

        // Spawn the coin trail if it fits
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
        // Determine the lane for the apple
        int lane = Random.Range(-1, 2);
        // Instantiate the apple
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

        // Return a random index for the tilePrefabs array
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        return randomIndex;
    }

    IEnumerator IncreaseSpawnRateOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // Increase spawn rate every 10 seconds
            currentSpawnRate += spawnRateIncreaseRate; // Increase the current spawn rate
        }
    }
}
