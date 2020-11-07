using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LineSpawner : MonoBehaviour
{
    public Transform SpawnPoint;

    private void OnMouseDown()
    {
        var agent = GameManager.instance.agentChooser.agentFactory.Create();
        GameManager.instance.pointsManager.ReducePoints(agent.cost);

        var game_unit = GameObject.Instantiate(agent, SpawnPoint.position, SpawnPoint.rotation, transform);

        Debug.Log("click line");
    }
}
