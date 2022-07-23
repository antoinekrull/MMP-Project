using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{

    private static readonly SpawnController _instance = new SpawnController();   
 
    public EnemyAI enemy; //needed to instantiate enemy objects   
    GlobalOptions globalOptions = GlobalOptions.GetInstance();

    Wave currentWave;
    private int waveCount;
    int currentWaveNumber = 0;  

    private SpawnController() { }

    public static SpawnController GetInstance()
    {
        return _instance;
    }

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
  
    // Start is called before the first frame update
    void Start()
    {
        waveCount = globalOptions.GetDifficulty() ? 5 : 10; //depending on difficulty
        startWave();      
    }

    Wave SetWavesAndEnemies(bool isNormalDifficulty)
    {
        int enemyCount = isNormalDifficulty ? 5 : 10;        
        return new Wave(enemyCount);             
    }


    void startWave()
    {
        Debug.Log(currentWaveNumber);
        if (currentWaveNumber < waveCount)
        {
            Debug.Log("New wave was set");
            currentWave = SetWavesAndEnemies(globalOptions.GetDifficulty());
            currentWaveNumber++;
        }
        else
        {
            SceneManager.LoadScene("Scenes/WinMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update wave counter or timer top right
        StartCoroutine(ExecuteAfterTime(0.8f, () =>
        {
            Debug.Log("Remaining Enemies to spawn: " + currentWave.remainingEnemiesToSpawn);
            currentWave.SpawnEnemy(enemy);
            if(currentWave.remainingEnemiesToSpawn > 0)
            {
                currentWave.remainingEnemiesToSpawn--;
            }           
        }));
    }

    void HandlePlayerDeath(PlayerController player)
    {
        SceneManager.LoadScene("Scenes/DeathMenu");
    }

    void HandleEnemyDefeated(EnemyAI enemy)
    {
        if (currentWave.enemies.Remove(enemy))
        {
            if (currentWave.enemies.Count == 0)
            {
                Debug.Log("Wave survived");
                startWave();
            }
        }
    }

    public class Wave
    {
        public int enemyCount;               
        public List<EnemyAI> enemies = new List<EnemyAI>();
        public int remainingEnemiesToSpawn;

        public void SpawnEnemy(EnemyAI enemy)
        {
            if(remainingEnemiesToSpawn > 0)
            {
                Vector2 position = new Vector2(0.5f, 12.5f);
                EnemyAI spawnedEnemy = Instantiate(enemy, position, Quaternion.identity) as EnemyAI;
                enemies.Add(spawnedEnemy);
            }            
        }

        public Wave(int eC)
        {
            enemyCount = eC;
            remainingEnemiesToSpawn = eC;
        }
    }


    private bool isCoroutineExecuting = false;
    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if(isCoroutineExecuting)
        {
            yield break;
        }
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }
}