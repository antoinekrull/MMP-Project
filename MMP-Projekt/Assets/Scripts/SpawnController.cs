using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{

    private static readonly SpawnController _instance = new SpawnController();
    public Wave[] waves = new Wave[10];
    public EnemyAI enemy;
    public List<EnemyAI> enemies = new List<EnemyAI>();
    GlobalOptions globalOptions = GlobalOptions.GetInstance();


    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int nextSpawnTime;

    private SpawnController() { }

    private void OnEnable()
    {
        EnemyAI.OnEnemyKilled += HandleEnemyDefeated;
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        EnemyAI.OnEnemyKilled -= HandleEnemyDefeated;
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }

    public static SpawnController GetInstance()
    {
        return _instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetWavesAndEnemies(globalOptions.GetDifficulty());
        nextWave();
    }

    void SetWavesAndEnemies(bool isNormalDifficulty)
    {

        if (isNormalDifficulty)
        {
            for (int i = 0; i < 5; i++)
            {
                waves[i] = new Wave(1, 5f);
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                waves[i] = new Wave(7, 5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            EnemyAI spawnedEnemy = Instantiate(enemy, Vector2.zero, Quaternion.identity) as EnemyAI;
            enemies.Add(spawnedEnemy);
            enemiesRemainingToSpawn--;
        }
    }

    void HandlePlayerDeath(PlayerController player)
    {
        SceneManager.LoadScene("Scenes/DeathMenu");
    }

    void HandleEnemyDefeated(EnemyAI enemy)
    {
        if (enemies.Remove(enemy))
        {
            if (enemies.Count == 0)
            {
                nextWave();
            }
            Debug.Log("Enemy killed");
        }
    }

    void nextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enemyCount;
        }
    }


    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;

        public Wave(int eC, float tBS)
        {
            enemyCount = eC;
            timeBetweenSpawns = tBS;
        }
    }
}