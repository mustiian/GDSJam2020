using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AgentChooser agentChooser;
    void Start()
    {
        agentChooser = new AgentChooser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
