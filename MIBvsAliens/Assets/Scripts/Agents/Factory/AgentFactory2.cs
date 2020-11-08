using System.Collections;
using System;
using UnityEngine;

public class AgentFactory2 : MonoBehaviour, IAgentFactory
{
    public GameObject prefab;

    public BaseAgent Create(Vector3 position, Vector3 endPosition)
    {
        Agent1 agent = prefab.GetComponent<Agent1>();

        if (GameManager.instance.pointsManager.HasRequiredPoints(agent.cost))
        {
            var gameObject = Instantiate(prefab, position, Quaternion.identity);
            var movement = gameObject.GetComponent<Movement>();
            movement.SetStartPosition(position);
            movement.SetDestination(endPosition);
            return agent;
        }
        else return null;
    }
}
