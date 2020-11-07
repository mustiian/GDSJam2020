using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentChooser : MonoBehaviour
{
    public Button buttonAgent1;
    public Button buttonAgent2;
    
    public AgentType currentAgentType;
    void Start()
    {
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentType(AgentType.Type1));
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentType(AgentType.Type2));
    }
    
    void ChangeCurrentAgentType(AgentType agentType)
    {
        currentAgentType = agentType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
