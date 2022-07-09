using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<EnemyAI> enemies = new List<EnemyAI>();
    
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
        if(enemies.remove(enemy))
        {
            // update counter when enemy got defeated
        }
    }

    private void Awake() 
    {
        enemies = GameObject.FindObjectsOfType<EnemyAI>().ToList();
        // update enemy counter text e.g.
    }
}
