using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LineSpawner : MonoBehaviour
{
    public Transform SpawnPoint;


    private void OnMouseDown()
    {
        var agent = GameManager.instance.agentChooser.agentFactory.Create(SpawnPoint.position);

        if (!agent)
            return;

        GameManager.instance.pointsManager.ReducePoints(agent.cost);
    }
}
