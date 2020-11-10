using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAlien : BaseCreature
{
    public AlienType type;
    
    public bool HasCow = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasCow)
            return;

        if (!GameManager.instance.cowsManager.HasFreeCow())
            return;

        if (collision.TryGetComponent(out Village creature))
        {
            HasCow = true;
            var controlSystem = GetComponent<UnitControlSystem>();
            controlSystem.MoveTurnAround();
            int currentSpeed = controlSystem.GetMovingSpeed();
            controlSystem.ChangeMovingSpeed(currentSpeed*2);
            
            GameManager.instance.cowsManager.PickupCow();
            Vector2 pos = transform.position + new Vector3(0f, 0.7f, 0f);
            GameObject.Instantiate(GameManager.instance.cowsManager.CowPrefab, pos, Quaternion.identity, transform);
        }
    }
}
