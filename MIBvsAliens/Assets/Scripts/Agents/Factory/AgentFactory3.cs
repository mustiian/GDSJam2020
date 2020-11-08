using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFactory3 : MonoBehaviour, IAgentFactory
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
