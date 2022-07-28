using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{

    private static readonly SpawnController _instance = new SpawnController();   
 
    public EnemyAI enemy; //needed to instantiate enemy objects   
    GlobalOptions globalOptions = GlobalOptions.GetInstance();

    Wave currentWave;
    private int waveCount;
    int currentWaveNumber = 0;
    public float survivedTime = 0f;
    public System.Random ran = new System.Random();

    public Text wavesEnemysLeft;

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
        StartWave();
    }

    Wave SetWavesAndEnemies(bool isNormalDifficulty)
    {
        int enemyCount = isNormalDifficulty ? 4 + currentWaveNumber * 2 : 5 + currentWaveNumber * 2;        
        return new Wave(enemyCount);             
    }


    void StartWave()
    {        
        if (currentWaveNumber < waveCount)
        {            
            currentWave = SetWavesAndEnemies(globalOptions.GetDifficulty());
            currentWaveNumber++;
            wavesEnemysLeft.text = waveCount - currentWaveNumber + 1 + " waves left\n" + currentWave.remainingEnemies + " enemys left\n\n" + globalOptions.playerHealth + " health left";
        }
        else
        {
            globalOptions.SetSurvivedTime(survivedTime);
            SceneManager.LoadScene("Scenes/WinMenu");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        survivedTime += Time.deltaTime;        
        //update wave counter or timer top right
        StartCoroutine(ExecuteAfterTime(ran.Next(3 , 15) * 0.2f, () =>
        {            
            currentWave.SpawnEnemy(enemy);                    
        }));        
        wavesEnemysLeft.text = waveCount - currentWaveNumber + 1 + " waves left\n" + currentWave.remainingEnemies + " enemys left\n\n" + globalOptions.playerHealth + " health left";
    }

    void HandlePlayerDeath(PlayerController player)
    {
        globalOptions.survivedWaves = currentWaveNumber-1;
        SceneManager.LoadScene("Scenes/DeathMenu");
    }

    void HandleEnemyDefeated(EnemyAI enemy)
    {
        if (currentWave.enemies.Remove(enemy))
        {
            currentWave.remainingEnemies--;
            if (currentWave.enemies.Count == 0)
            {
                StartCoroutine(DelayWave(2f, () =>
                {
                    StartWave();
                }));
                
            }
        }
    }

    public class Wave
    {
        public int enemyCount;               
        public List<EnemyAI> enemies = new List<EnemyAI>();
        public int remainingEnemiesToSpawn;
        GlobalOptions globalOptions = GlobalOptions.GetInstance();
        private System.Random ran = new System.Random();
        public int remainingEnemies;
        public bool isFinalWave;

        public void SpawnEnemy(EnemyAI enemy)
        {
            if(remainingEnemiesToSpawn > 0)
            {
                EnemyAI spawnedEnemy = Instantiate(enemy, globalOptions.GetDifficulty() ? new Vector2(0.5f, 12.5f) : new Vector2(ran.Next(-21, 40), ran.Next(-15, 3)), Quaternion.identity) as EnemyAI;                             
                enemies.Add(spawnedEnemy);
                remainingEnemiesToSpawn--;                
            }            
        }

        public void SpawnBossEnemy(EnemyAI enemy)
        {
            EnemyAI spawnedEnemy = Instantiate(enemy, globalOptions.GetDifficulty() ? new Vector2(0.5f, 12.5f) : new Vector2(ran.Next(-21, 40), ran.Next(-15, 3)), Quaternion.identity) as EnemyAI;
            Vector3 localScale = spawnedEnemy.gameObject.transform.localScale;
            spawnedEnemy.maxHealth = 3;
            spawnedEnemy.attackRadius = 4;
            spawnedEnemy.gameObject.transform.localScale = localScale * 3;
            remainingEnemies++;
            enemies.Add(spawnedEnemy);
        }

        public Wave(int eC)
        {
            enemyCount = eC;
            remainingEnemiesToSpawn = eC;
            remainingEnemies = eC;
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

    private bool isDelayWaveExecuting = false;
    IEnumerator DelayWave(float time, Action task)
    {
        if (isDelayWaveExecuting)
        {
            yield break;
        }
        isDelayWaveExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isDelayWaveExecuting = false;
    }

}