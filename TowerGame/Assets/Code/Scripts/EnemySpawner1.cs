using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WavePrefabRow[] waveRows;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSec = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 0;
    private int enemiesAlive;
    private float timeSinceLastSpawn;
    private int enemiesLeftToSpawn;
    private int enemiesSpawned = 0;
    private bool isSpawning = false;


    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesSpawned = 0;
    }

    private int EnemiesPerWave()
    {
        return waveRows[currentWave].wavePrefabs.Length;
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSec) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
            enemiesSpawned++;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = waveRows[currentWave].wavePrefabs[enemiesSpawned];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        LevelManager.main.IncreaseCurrency(50);
        if (currentWave >= waveRows.Length)
        {
            Debug.Log("Level Finished");
        }
        else
        {
            StartCoroutine(StartWave());
        }
    }
}
