using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _velocitySetPoint = 3f;
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _enemyMicrojump = 2f;
    [SerializeField] private bool _isDoubleJumpEnabled = true;

    protected Vector2 _targetVelocity;
    protected bool _isInGround;
    protected Vector2 _groundNormal;
    protected Rigidbody2D _rigidBody2D;
    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private BoxCollider2D _foot;
    private int _jumpCount;
    private int _jumpCountSingle = 1;
    private int _jumpCountDouble = 2;
    private Animator _animator;
    private HashAnimationNames animationNames = new HashAnimationNames();

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.001f;

    void OnEnable()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        JumpCountSet();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal") * _velocitySetPoint, 0);

        if (_targetVelocity != Vector2.zero)
        {
            _animator.SetTrigger(animationNames.WalkHash);
        }

        if (_isInGround && _jumpCount == 0)
            JumpCountSet();

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount > 0)
        {
            _velocity.y = _jumpVelocity;
            _jumpCount--;
            _animator.SetTrigger(animationNames.JumpHash);           
        }        
    }

    void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;
                
        _isInGround = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);        
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = _rigidBody2D.Cast(move, _contactFilter, _hitBuffer, distance + shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _isInGround = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody2D.position = _rigidBody2D.position + move.normalized * distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _velocity.y = _enemyMicrojump;            
    }

    private void JumpCountSet()
    {
        if (_isDoubleJumpEnabled)        
            _jumpCount = _jumpCountDouble;
        else
            _jumpCount = _jumpCountSingle;

        Debug.Log($"Jump count seted as {_jumpCount}");        
    }   
}