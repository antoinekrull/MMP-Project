using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{

    private static readonly SpawnController _instance = new SpawnController();
    public Wave[] waves;
    public EnemyAI enemy;
    public List<EnemyAI> enemies = new List<EnemyAI>();

    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int nextSpawnTime;

    private SpawnController() {}

    private void OnEnable() 
    {
        EnemyAI.OnEnemyKilled += HandleEnemyDefeated;
        EnemyAI.OnDamageTaken += HandleEnemyDamageTaken;
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
        PlayerController.OnDamageTaken += HandlePlayerDamageTaken;
    }

    private void OnDisable() 
    {
        EnemyAI.OnEnemyKilled -= HandleEnemyDefeated;
        EnemyAI.OnDamageTaken -= HandleEnemyDamageTaken;
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
        PlayerController.OnDamageTaken -= HandlePlayerDamageTaken;
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
            enemies.Add(spawnedEnemy);
            enemiesRemainingToSpawn--;
        }
    }

    void HandleEnemyDamageTaken(EnemyAI enemy)
    {
        enemy.TakeDamage(1);
    }

    void HandlePlayerDamageTaken(PlayerController player)
    {
        player.TakeDamage(1);
    }

    void HandleEnemyDefeated(EnemyAI enemy) 
    {
        if(enemies.Remove(enemy))
        {
            if(enemies.Count == 0) {
                nextWave();
            }
            Debug.Log("Enemy killed");
        }
    }

    void HandlePlayerDeath(PlayerController player)
    {
        //insert tick methon if deathmenu is loaded too fast
        SceneManager.LoadScene("Scenes/DeathMenu");
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
