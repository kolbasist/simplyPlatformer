using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolByPath : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] float _speed;

    private Transform[] _wayPoints;
    private int _wayPointsCount;
    private int _currentWayPoint = 0;
            
    private void Start()
    {
        _wayPointsCount = _path.childCount;
        _wayPoints = new Transform[_wayPointsCount];

        for (int i = 0; i < _wayPointsCount; i++)
        {
            _wayPoints[i] = _path.GetChild(i);
        }
    }
        
    private void Update()
    {
        Transform target = _wayPoints[_currentWayPoint];
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        Debug.Log( transform.position - target.position );

        if (transform.position == target.position)
        {
            _currentWayPoint++;

            if (_currentWayPoint >= _wayPointsCount)
                _currentWayPoint = 0;
        }       
    }
}
