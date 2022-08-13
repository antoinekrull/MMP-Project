using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalOptions
{
    public float survivedTime { get; set; } = 0f;
    public int survivedWaves { get; set; }
    public int playerHealth { get; set; }
    public bool isNormalDifficulty { get; set; } = true;
    public enum gameStates { start, victory, defeat };
    public int gameState { get; set; } = (int)gameStates.start;

    private GlobalOptions() { /*Prevent outside instantiation*/ }

    private static readonly GlobalOptions _instance = new GlobalOptions();

    public static GlobalOptions GetInstance()
    {
        return _instance;
    }
}
