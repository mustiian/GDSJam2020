using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseAlien alien) &&
            collision.TryGetComponent(out UnitControlSystem controlSystem))
        {
            if (alien.HasCow)
            {
                alien.HasCow = false;
                GameManager.instance.cowsManager.DecreaseCows();
                //hide the alien here
                controlSystem.RequestDestroy();
            }
        }
    }
}
