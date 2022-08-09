using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayersFoot : MonoBehaviour
{
    [SerializeField] private float _contrForce;        

    private Rigidbody2D _rigidbody;    

    private void Start()
    {
        _rigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy.gameObject);
            _rigidbody.AddForce(Vector2.up * _contrForce);
        }       
    }
}
