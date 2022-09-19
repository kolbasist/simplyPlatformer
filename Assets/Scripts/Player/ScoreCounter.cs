using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public uint Score { get; private set; }

    private void Start()
    {
        Score = 0;
    }

    public void GetCoin(Coin coin)
    {
        Score += coin.Cost;
        Debug.Log($"score: {Score}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            GetCoin(coin);
            coin.gameObject.SetActive(false);
        }
    }
}
