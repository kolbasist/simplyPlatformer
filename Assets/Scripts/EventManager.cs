using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

static public class EventManager 
{
    public static UnityEvent<int> ScorePointsGetted = new UnityEvent<int>();    
    
    public static void SendEnemyKilled(int weight)
    {
        ScorePointsGetted?.Invoke(weight);
    }
   
    public static void SendCoinPicked(int weight)
    {
        ScorePointsGetted?.Invoke(weight);
    }
}
