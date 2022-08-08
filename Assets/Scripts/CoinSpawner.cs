using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeF+3ield] private uint _coinCount = 12;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private Transform _points;    

    private Transform[] _pointsArray;
    private int _pointsCount;
    private int _spawnIndex = 0;

    private void Start()
    {
        PointsParser pointsParser = new PointsParser();
        _pointsArray = pointsParser.Parse(_points, out _pointsCount);        
    }

    private IEnumerator SpawnQueue(uint count)
    {
        var waitForFewSeconds = new WaitForSeconds(_duration);

        for (int i = 0; i < count; i++)
        {
            Coin newCoin = Instantiate(_template, _pointsArray[_spawnIndex].position, Quaternion.identity);
            _spawnIndex++;

            if (_spawnIndex >= _pointsCount)
            {
                _spawnIndex = 0;
            }

            yield return waitForFewSeconds;
        }
    }
}
