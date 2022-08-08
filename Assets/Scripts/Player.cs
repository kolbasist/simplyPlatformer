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
            transform.Translate(_speed * Time.deltaTime , 0, 0);

            if (_isFacingRight == true)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _animator.SetTrigger("Walk");
            transform.Translate(_speed * Time.deltaTime* -1, 0, 0);

            if (_isFacingRight == false)
                Flip();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
}
