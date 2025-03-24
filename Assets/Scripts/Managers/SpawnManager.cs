using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleData
{
    public GameObject obstaclePrefab;
    public float minSpawnRate; // Valor mínimo para spawn rate
    public float maxSpawnRate; // Valor máximo para spawn rate
    public float spawnYPosition; // Posición Y de spawn
}

public class SpawnManager : MonoBehaviour
{
    public List<ObstacleData> obstacles;

    private List<float> spawnTimers;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnTimers = new List<float>();

        // Inicializa los timers de spawn para cada obstáculo
        foreach (var obstacle in obstacles)
        {
            // Genera un spawn rate aleatorio dentro del rango definido para este obstáculo
            float randomSpawnRate = Random.Range(obstacle.minSpawnRate, obstacle.maxSpawnRate);
            spawnTimers.Add(randomSpawnRate);
        }
    }

    void Update()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            spawnTimers[i] -= Time.deltaTime;

            if (spawnTimers[i] <= 0)
            {
                if (playerControllerScript.isAlive)
                {
                    SpawnObstacle(obstacles[i]);

                    // Genera un nuevo spawn rate aleatorio dentro del rango definido
                    float randomSpawnRate = Random.Range(obstacles[i].minSpawnRate, obstacles[i].maxSpawnRate);
                    spawnTimers[i] = randomSpawnRate;
                }
            }
        }
    }

    void SpawnObstacle(ObstacleData obstacleData)
    {
        Vector3 spawnPos = new Vector2(10, obstacleData.spawnYPosition);
        Instantiate(obstacleData.obstaclePrefab, spawnPos, obstacleData.obstaclePrefab.transform.rotation);
    }
}

