using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAlien : BaseCreature
{
    public bool HasCow = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasCow)
            return;

        if (collision.TryGetComponent(out Village creature))
        {
            var transform = gameObject.GetComponent<Transform>();
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            
            HasCow = true;
            state = State.MovingBack;
            GameManager.instance.cowsManager.PickupCow();
            Vector2 pos = transform.position + new Vector3(0f, 0.7f, 0f);
            GameObject.Instantiate(GameManager.instance.cowsManager.CowPrefab, pos, Quaternion.identity, transform);
        }
    }
}
