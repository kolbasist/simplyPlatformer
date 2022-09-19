using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawner : ObjectPool
{
    [SerializeField] private Coin _template;    
    [SerializeField] private Transform _points;    

    private Transform[] _pointsArray;
    private int _pointsCount;
    private int _spawnIndex = 0;

    private void Start()
    {
        GameObject[] prefabs = new GameObject[1];
        prefabs[0] = _template.gameObject;

        Initialize(prefabs);

        PathPointsParser pointsParser = new PathPointsParser();
        _pointsArray = pointsParser.Parse(_points, out _pointsCount);
    }    
}
