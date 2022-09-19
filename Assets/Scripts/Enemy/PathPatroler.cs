using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPatroler : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed = 1f;

    private Transform[] _wayPoints;
    private int _wayPointsCount;
    private int _currentWayPoint = 0;
    private Transform _target;

    private void Start()
    {
        PathPointsParser pointsParser = new PathPointsParser();
        _wayPoints = pointsParser.Parse(_path, out _wayPointsCount);
        SetTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        Vector3 direction = transform.position - _target.position;

        if (transform.position == _target.position)
        {
            _currentWayPoint++;

            if (_currentWayPoint >= _wayPointsCount)
                _currentWayPoint = 0;

            SetTarget();
        }
    }

    private void SetTarget()
    {
        _target = _wayPoints[_currentWayPoint];
    }    

    public void SetParameters(Transform path, float speed = 1f)
    {
        if (_path == null && path.childCount != 0 && path != null)
        {
            _path = path;
            _speed = speed;
        }
            
    }
}
