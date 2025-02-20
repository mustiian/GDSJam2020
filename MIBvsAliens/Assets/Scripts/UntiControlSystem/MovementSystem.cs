﻿using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private bool _initialized = false; 
    
    private Transform _transform;
    private Vector3 _toPoint;
    private Vector3 _fromPoint;
    private bool _isMoving;
    private Animator _movingAnimator;
    
    public int speed;

    public void Initialize(Transform objectToMove, Vector3 fromPoint,
        Vector3 toPoint, int initSpeed, Animator movingAnimator)
    {
        if (_initialized)
            return;
        
        _transform = objectToMove;
        _fromPoint = fromPoint;
        _toPoint = toPoint;
        speed = initSpeed;
        _movingAnimator = movingAnimator;

        _initialized = true;
    }

    public void Enable()
    {
        if (_movingAnimator)
        _movingAnimator.Play("Walk");
        _isMoving = true;
    }

    public void Disable()
    {
        _isMoving = false;
    }

    public void TurnAround()
    {
        Vector3 tmp = _fromPoint;
        _fromPoint = _toPoint;
        _toPoint = tmp;
        _transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    }

    void FixedUpdate()
    {
        if (!_initialized)
            return;
        
        if (!_isMoving)
            return;

        float step = 0.5f * speed * Time.fixedDeltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _toPoint, step);
    }
}
