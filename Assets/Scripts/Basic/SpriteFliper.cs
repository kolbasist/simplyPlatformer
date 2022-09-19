using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFliper :MonoBehaviour
{   
    private bool _isFacingRight = true;
    private float _previousPosition;

    private void Start()
    {
        _previousPosition = gameObject.transform.position.x;
    }

    private void Update()
    {
        float horizontaldirection = gameObject.transform.position.x - _previousPosition;

        if (horizontaldirection >0 && _isFacingRight == false)        
            Flip();
        else if (horizontaldirection < 0 && _isFacingRight)
            Flip();

        _previousPosition = gameObject.transform.position.x;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _isFacingRight = !_isFacingRight;
    }
}
