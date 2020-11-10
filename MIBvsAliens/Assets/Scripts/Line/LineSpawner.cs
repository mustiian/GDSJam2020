using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class LineSpawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform EndPoint;
    private float _yOffset = 0.2f;
    private int sortingLayer = 1;

    private void OnMouseDown()
    {
        var position = SpawnPoint.position;
        var agent = GameManager.instance.agentChooser.agentFactory.Create(
            new Vector3(position.x, position.y + _yOffset), EndPoint.position);
        GenerateNewOffset();
        if (!agent)
            return;
        GameManager.instance.pointsManager.ReducePoints(agent.cost);
        agent.GetComponent<SortingGroup>().sortingOrder += sortingLayer;
    }

    private int _offsetCount = 0;
    private void GenerateNewOffset()
    {
        if (_offsetCount < 3)
        {
            _yOffset -= 0.2f;
            _offsetCount++;
        }
        else
        {
            _yOffset = 0.2f;
            _offsetCount = 0;
        }
    }
}
