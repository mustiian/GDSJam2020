using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentChooser : MonoBehaviour
{
    public Button buttonAgent1;
    public Button buttonAgent2;

    public IAgentFactory agentFactory;
    void Start()
    {
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentFactory(gameObject.AddComponent<AgentFactory1>()));
        buttonAgent2.onClick.AddListener(() => ChangeCurrentAgentFactory(gameObject.AddComponent<AgentFactory2>()));
    }
    
    void ChangeCurrentAgentFactory(IAgentFactory newAgentFactory)
    {
        Debug.Log("Agent changed");
        agentFactory = newAgentFactory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
