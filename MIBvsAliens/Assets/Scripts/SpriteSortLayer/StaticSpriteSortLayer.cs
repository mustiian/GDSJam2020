using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StaticSpriteSortLayer : MonoBehaviour
{
    void Start()
    {
        if(TryGetComponent(out SortingGroup group)){
            group.sortingOrder += Mathf.Abs((int)(100 * 1/transform.position.y));
        }
        else if (TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.sortingOrder += Mathf.Abs((int)(100 * 1/transform.position.y));
        }
    }
}
