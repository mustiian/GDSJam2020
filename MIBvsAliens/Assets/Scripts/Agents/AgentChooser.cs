using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentChooser : MonoBehaviour
{
    public Button buttonAgent1;
    public Button buttonAgent2;
    public Button buttonAgent3;
    public Button buttonAgent4;

    public IAgentFactory agentFactory;
    void Start()
    {
        buttonAgent1.onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactory1>()));
        buttonAgent2.onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactory2>()));
        buttonAgent3.onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactory3>()));
        buttonAgent4.onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactory4>()));
    }
    
    void ChangeCurrentAgentFactory(IAgentFactory newAgentFactory)
    {
        Debug.Log("Agent changed" + newAgentFactory.ToString());
        agentFactory = newAgentFactory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
