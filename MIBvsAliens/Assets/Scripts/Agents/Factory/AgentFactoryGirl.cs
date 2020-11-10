using UnityEngine;

public class AgentFactoryGirl : MonoBehaviour, IAgentFactory
{
    public GameObject[] prefab;

    public BaseAgent Create(Vector3 position, Vector3 endPosition)
    {
        BaseAgent agent = prefab[1].GetComponent<BaseAgent>();

        if (GameManager.instance.pointsManager.HasRequiredPoints(agent.cost))
        {
            int index = Random.Range(0, prefab.Length);

            var gameObject = Instantiate(prefab[index], position, Quaternion.identity);
            var controlSystem = gameObject.GetComponent<UnitControlSystem>();
            controlSystem.Initialize(position, endPosition);
            return agent;
        }
        else return null;
    }
}
