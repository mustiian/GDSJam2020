using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AgentFactory1 : MonoBehaviour, IAgentFactory
{
    public GameObject prefab;

    public BaseAgent Create(Vector3 position)
    {
        Debug.Log("prefab " + prefab.name);

        var agent = GameObject.Instantiate(prefab, position, Quaternion.identity);

        return agent.GetComponent<Agent1>();
    }
}
