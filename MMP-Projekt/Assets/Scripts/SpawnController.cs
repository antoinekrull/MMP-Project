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
        return new Wave(enemyCount, enemy);             
    }


    void startWave()
    {
        Debug.Log(currentWaveNumber);
        if (currentWaveNumber < waveCount)
        {
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
                startWave();
            }
        }
    }

    public class Wave
    {
        public int enemyCount;               
        public List<EnemyAI> enemies = new List<EnemyAI>();

        public Wave(int eC, EnemyAI enemy)
        {
            enemyCount = eC;         

            for(int i = 0; i < enemyCount; i++)
            {
                EnemyAI spawnedEnemy = Instantiate(enemy, Vector2.zero, Quaternion.identity) as EnemyAI;
                enemies.Add(spawnedEnemy);
            }                       
        }
    }
}