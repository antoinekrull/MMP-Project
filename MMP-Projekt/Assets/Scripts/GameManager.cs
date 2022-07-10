using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static readonly GameManager _instance = new GameManager();

    private GameManager()
    {

    }

    public static GameManager GetInstance()
    {
        return _instance;
    }


    private void Update() 
    {
    
    }
}
