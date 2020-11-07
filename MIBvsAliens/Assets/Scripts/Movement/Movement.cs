using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform _transform;
    private BaseCreature _creatureToMove;
    private int _direction = 1;
    private Time _lastMove;
    
    public void GoLeft()
    {
        _direction = -1;
    }
    
    public void GoRight()
    {
        _direction = 1;
    }

    public void MoveTo(Vector3 pointTo)
    {
        Debug.Log("123");
        StartCoroutine(MoveFromTo(_transform, _transform.position, pointTo, 0.4f));
    }

    private void Awake()
    {
        Debug.Log("1777");
        _transform = gameObject.GetComponent<Transform>();
    }


    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed) {
        Debug.Log("Started");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f) {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
