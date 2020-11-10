using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseAlien alien))
        {
            if (alien.HasCow)
            {
                alien.HasCow = false;
                GameManager.instance.cowsManager.DecreaseCows();
                Destroy(collision.gameObject);
            }
        }
    }
}
