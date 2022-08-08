using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 1.0f;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float _jumpForce = 10f;

    private bool _isDead = false;
    private Animator _animator;
    private bool _isFacingRight;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_isDead)
            Destroy(gameObject);

        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetTrigger("Walk");
            _rigidbody.AddForce(Vector2.right * _speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetTrigger("Walk");
            _rigidbody.AddForce(Vector2.left * _speed);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.A) && _isFacingRight == false)
            Flip();
        else if (Input.GetKeyDown(KeyCode.D) && _isFacingRight == true)
            Flip();

        
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _isFacingRight = !_isFacingRight;
    }
}
