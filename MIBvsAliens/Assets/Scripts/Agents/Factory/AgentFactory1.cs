using UnityEngine;

public class AgentFactory1 : MonoBehaviour, IAgentFactory
{
    public GameObject[] prefab;

    public BaseAgent Create(Vector3 position, Vector3 endPosition)
    {
        Agent1 agent = prefab[1].GetComponent<Agent1>();

        if (GameManager.instance.pointsManager.HasRequiredPoints(agent.cost))
        {
            int index = Random.Range(0, prefab.Length);

            var gameObject = Instantiate(prefab[index], position, Quaternion.identity);
            var movement = gameObject.GetComponent<Movement>();
            movement.SetStartPosition(position);
            movement.SetDestination(endPosition);
            return agent;
        }
        else return null;
    }
}
