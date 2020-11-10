using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentsInfoGetter : MonoBehaviour
{
    private Dictionary<AgentType, CharacterInfo> _infos;

    private void Awake()
    {
        var charsInfo = GameManager.instance.charactersInfo.agentInfo;
        _infos = charsInfo.ToDictionary(info => info.agentType, info => info.characterInfo);
    }

    public CharacterInfo GetFor(AgentType agentType)
    {
        return _infos[agentType];
    }
}