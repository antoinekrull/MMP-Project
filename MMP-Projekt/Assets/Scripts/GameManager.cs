using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    List<EnemyAI> enemies = new List<EnemyAI>();
    private static readonly GameManager _instance = new GameManager();

    private GameManager()
    {

    }

    public GameManager GetInstance()
    {
        return _instance;
    }

    private void OnEnable() 
    {
        EnemyAI.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable() 
    {
        EnemyAI.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void HandleEnemyDefeated(EnemyAI enemy) 
    {
        if(enemies.Remove(enemy))
        {
            // update counter when enemy got defeated
        }
    }

    private void Awake() 
    {
        enemies = GameObject
                    .FindObjectsOfType<EnemyAI>()
                    .ToList();
        // update enemy counter text e.g.
    }
}
