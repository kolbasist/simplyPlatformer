using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;    
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private uint _maxJumpCount = 3;
    [SerializeField] private LayerMask _contactFilter;

    public uint Score { get; private set; }

    private bool _isDead = false;
    private Animator _animator;
    private bool _isFacingRight;
    private BoxCollider2D _footCollider;
    private int _jumpCount;
    private bool _isTouchingGround;
    private Rigidbody2D _rigidbody;

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
        if (_isDead)
            Destroy(gameObject);

        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetTrigger("Walk");
            transform.Translate(_speed * Time.deltaTime, 0, 0);

            if (_isFacingRight == true)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetTrigger("Walk");
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);

            if (_isFacingRight == false)
                Flip();
        }
        _isTouchingGround = Physics2D.OverlapCircle(_footCollider.transform.position, _footCollider.size.y, _contactFilter);

        if (_isTouchingGround == true)
        {
            _jumpCount = (int)_maxJumpCount;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount > 0)
        {
            _jumpCount--;           
            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }    

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _isFacingRight = !_isFacingRight;
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
            Destroy(coin.gameObject);
        }
    }
}
