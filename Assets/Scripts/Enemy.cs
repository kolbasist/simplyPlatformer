using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _eventManager;
    [SerializeField] private int scoreWeight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Destroy(player.gameObject);
        }
    }

    private void OnDestroy()
    {
        //_eventManager += scoreWeight;
    }
}
