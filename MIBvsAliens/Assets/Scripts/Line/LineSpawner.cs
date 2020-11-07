using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LineSpawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform EndPoint;
    private float _yOffset = -0.6f;

    private void OnMouseDown()
    {
        var position = SpawnPoint.position;
        var agent = GameManager.instance.agentChooser.agentFactory.Create(
            new Vector3(position.x, position.y + _yOffset), EndPoint.position);
        GenerateNewOffset();
        if (!agent)
            return;
        //GameManager.instance.pointsManager.ReducePoints(agent.cost);
    }

    private int _offsetCount = 0;
    private void GenerateNewOffset()
    {
        if (_offsetCount < 4)
        {
            _yOffset += 0.3f;
            _offsetCount++;
        }
        else
        {
            _yOffset = -0.6f;
            _offsetCount = 0;
        }

    }
}
