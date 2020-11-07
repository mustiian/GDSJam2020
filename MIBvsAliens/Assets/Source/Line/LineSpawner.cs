using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LineSpawner : MonoBehaviour
{
    public Transform SpawnPoint;

    private void OnMouseDown()
    {
        // GameObject unit;
        // var game_unit = GameObject.Instantiate(unit, SpawnPoint.position, SpawnPoint.rotation, transform);
        // Decrease points

        Debug.Log("click line");
    }
}
