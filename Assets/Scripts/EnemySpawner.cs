using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    [Header("Enemy Spawn Settings")]
    public GameObject enemyPrefab;

    [Tooltip("Units in seconds")]
    public float spawnRate = 5.0f;

    [Tooltip("Event registering string when game starts")]
    public string gameStartTag = "gameStart";

    [Tooltip("Event registering string when game ends")]
    public string gameEndTag = "gameEnd";

    private bool spawnEnabledFlag = false;

    private float currentSpawnTimer;

    public override void OnStartServer()
    {
        Debug.Log("Registering Events");
        EventManager.StartListening(gameStartTag, EnableSpawn);
        EventManager.StartListening(gameEndTag, DisableSpawn);
    }

    void EnableSpawn()
    {
        spawnEnabledFlag = true;
    }

    void DisableSpawn()
    {
        spawnEnabledFlag = false;
    }

    void SpawnEnemy()
    {
        // Generate a random location that is 10 units away from center
        var spawnPosition = new Vector3(
            Random.Range(3, 8) * Random.Range(0, 2) * 2 - 1,   // Generate a random x coord from center, can be + or -
            0.0f,
            Random.Range(3, 8) * Random.Range(0, 2) * 2 - 1);  // Generate a random z coord from center, can be + or -

        var spawnRotation = Quaternion.Euler(0f, 0f, 0f);

        // Spawn enemy prefab
        var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        // Sync with server
        NetworkServer.Spawn(enemy);
    }

    void Start()
    {
        currentSpawnTimer = spawnRate;
    }

    void Update()
    {
        // Start spawning if flag is enabled
        if (spawnEnabledFlag)
        {
            // Count down spawn timer
            if (currentSpawnTimer > 0)
            {
                currentSpawnTimer -= Time.deltaTime;
            }
            else
            {
                // Calls spawn routine
                SpawnEnemy();

                // Resets spawn rate
                currentSpawnTimer = spawnRate;
            }
        }
    }
}
