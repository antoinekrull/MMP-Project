using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    private static readonly SpawnController _instance = new SpawnController();
    public Wave[] waves;
    public EnemyAI enemy;


    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int nextSpawnTime;
    GameManager manager = GameManager.GetInstance();

    private SpawnController()
    {

    }

    public static SpawnController GetInstance()
    {
        return _instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        nextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            EnemyAI spawnedEnemy = Instantiate(enemy, Vector2.zero, Quaternion.identity) as EnemyAI;
            enemiesRemainingToSpawn--;
        }
    }

    void OnEnemyDeath()
    {

    }

    void nextWave()
    {
        currentWaveNumber++;
        if(currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enemyCount;
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
