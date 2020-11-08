using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAlien : BaseCreature
{
    public bool HasCow = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Village creature))
        {
            HasCow = true;
            state = State.MovingBack;
            GameManager.instance.cowsManager.PickupCow();
        }
    }
}
