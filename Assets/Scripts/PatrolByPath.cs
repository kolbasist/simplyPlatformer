using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolByPath : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _isDebuging;    

    private Transform[] _wayPoints;
    private int _wayPointsCount;
    private int _currentWayPoint = 0;

    private void Start()
    {
        PointsParser pointsParser = new PointsParser();
        _wayPoints = pointsParser.Parse(_path, out _wayPointsCount);        
    }

    private void Update()
    {
        Transform target = _wayPoints[_currentWayPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (_isDebuging)
            Debug.Log(transform.position - target.position);

        if (transform.position == target.position)
        {
            _currentWayPoint++;

            if (_currentWayPoint >= _wayPointsCount)
                _currentWayPoint = 0;
        }
    }
}
