using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button buttonAgent1;
    public Button buttonAgent2;
    public AgentType currentAgentType;
    void Start()
    {
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentType(AgentType.Type1));
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
