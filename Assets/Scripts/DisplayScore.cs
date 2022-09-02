using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    private int _score = 0;

    private void Start()
    {
        int weight = 100;
        EventManager.OnEnemyKiled.AddListener();
    }

    private void UpdateText()
    {
        GetComponent<Text>().text = "Score: " + _score.ToString();
    }

    private void EnemyKilled (int enemyWeight)
    {
        _score += enemyWeight;
        UpdateText();
    }

    private void CoinPicked( int costOfCoin)
    {
        _score += costOfCoin;
        UpdateText ();
    }
}
