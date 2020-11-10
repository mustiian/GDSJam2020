using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentChooser : MonoBehaviour
{
    public Button[] buttons;

    public IAgentFactory agentFactory;
    void Start()
    {
        buttons[0].onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactoryOld>()));
        buttons[1].onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactoryGirl>()));
        buttons[2].onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactoryBlack>()));
        buttons[3].onClick.AddListener(() => ChangeCurrentAgentFactory(FindObjectOfType<AgentFactoryBald>()));
    }

    public void EnableButton(int i)
    {
        buttons[i].interactable = true;
    }

    public void DisableButton(int i)
    {
        buttons[i].interactable = false;
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
