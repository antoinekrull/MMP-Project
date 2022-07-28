using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalOptions
{
    private float survivedTime = 0f;
    public int survivedWaves { get; set; }
    public int playerHealth { get; set; }

    private GlobalOptions()
    {
        // Prevent outside instantiation
    }

    private static readonly GlobalOptions _instance = new GlobalOptions();

    public static GlobalOptions GetInstance()
    {
        return _instance;
    }

    private static bool isNormalDifficulty = true;

    public void SetDifficulty(bool isNormal) {
        isNormalDifficulty = isNormal;
    }

    public bool GetDifficulty() {
        return isNormalDifficulty;
    }

    public void SetSurvivedTime(float time)
    {
        survivedTime = time;
    }

    public float GetSurvivedTime()
    {
        return survivedTime;
    }
}
