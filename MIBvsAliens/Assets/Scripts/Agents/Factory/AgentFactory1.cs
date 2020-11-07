using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AgentFactory1 : MonoBehaviour, IAgentFactory
{
    private Agent1 agent;

    public BaseAgent Create()
    {
        return agent;
    }
    
    void Start()
    {
        agent = GetComponent<Agent1>();
    }
}
