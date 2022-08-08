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
    private Transform _target;
    private bool _isFacingRight = false;

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

        if (direction.x < 0 && _isFacingRight == true)
            Flip();
        else if (direction.x > 0 && _isFacingRight == false)
            Flip();


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

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _isFacingRight = !_isFacingRight;
    }
}
