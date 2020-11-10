using UnityEngine;

public class AgentsInfoGetter : MonoBehaviour
{
    public CharacterInfo GetFor(AgentType agentType)
    {
        return new CharacterInfo()
        {
            damage = 5,
            health = 10,
            speed = 2
        };
    }
}