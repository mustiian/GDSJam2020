using UnityEngine;

public class AgentFactory1 : MonoBehaviour, IAgentFactory
{
    public GameObject prefab;

    public BaseAgent Create(Vector3 startPosition, Vector3 endPosition)
    {
        Agent1 agent = prefab.GetComponent<Agent1>();

        if (GameManager.instance.pointsManager.HasRequiredPoints(agent.cost))
        {
            var gameObject = Instantiate(prefab, startPosition, Quaternion.identity);
            var movement = gameObject.GetComponent<Movement>();
            movement.SetStartPosition(startPosition);
            movement.SetDestination(endPosition);
            agent.PlayMoveAnimation();
            return agent;
        }
        else return null;
    }
}
