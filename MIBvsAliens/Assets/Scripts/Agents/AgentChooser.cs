using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentChooser
{
    public Button buttonAgent1;
    public Button buttonAgent2;

    public IAgentFactory agentFactory;
    void Start()
    {
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentType(new AgentFactory1()));
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentType(new AgentFactory2()));
    }
    
    void ChangeCurrentAgentType(IAgentFactory newAgentFactory)
    {
        Debug.Log("Agent changed");
        agentFactory = newAgentFactory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
