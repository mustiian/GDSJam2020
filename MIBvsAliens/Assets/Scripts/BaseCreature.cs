using System;
using UnityEngine;

public class BaseCreature : MonoBehaviour
{
    public int damage;
    public float speed;
    public Health health;
    public int cost;
    
    private Movement _movement;

    private void Start()
    {
        _movement = gameObject.AddComponent<Movement>();
    }

    public void MoveTo(Vector3 pointTo)
    {
        _movement.MoveTo(pointTo);
    }
}
