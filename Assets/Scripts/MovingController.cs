using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Rigidbody2D))]

public class MovingController : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;    
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private uint _maxJumpCount = 3;
    [SerializeField] private LayerMask _contactFilter;

    public uint Score { get; private set; }
        
    private Animator _animator;    
    private BoxCollider2D _footCollider;
    private int _jumpCount;
    private bool _isTouchingGround;
    private Rigidbody2D _rigidbody;
    private HashAnimationNames _animationNames = new HashAnimationNames();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _footCollider = GetComponentInChildren<PlayersFoot>().GetComponent<BoxCollider2D>();
        _jumpCount = (int)_maxJumpCount;
        Score = 0;
    }
    private void Update()
    {  
        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetTrigger(_animationNames.WalkHash);
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetTrigger(_animationNames.WalkHash);
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);           
        }

        _isTouchingGround = Physics2D.OverlapCircle(_footCollider.transform.position, _footCollider.size.y, _contactFilter);

        if (_isTouchingGround)
        {
            _jumpCount = (int)_maxJumpCount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount > 0)
        {
            _jumpCount--;           
            _animator.SetTrigger(_animationNames.JumpHash);
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    } 
}
