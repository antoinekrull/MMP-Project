using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public Wave[] waves;
    //Enemy is a dummy class atm, waiting for a real class by @Leon
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int nextSpawnTime;

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
            Enemy spawnedEnemy = Instantiate(enemy, Vector2.zero, Quaterion.identity) as Enemy;

        }
    }

    void nextWave()
    {
        currentWaveNumber++;
        if(currentWaveNumber - 1 < waves.length)
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
