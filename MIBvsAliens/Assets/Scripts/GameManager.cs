using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public AgentChooser agentChooser;
    [HideInInspector]public PointsManager pointsManager;
    [HideInInspector]public CowsManager cowsManager;
    
    [HideInInspector]public AgentsInfoGetter agentsInfoGetter;
    [HideInInspector]public AliensInfoGetter aliensInfoGetter;
    
    public static GameManager instance;
    


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        agentChooser = FindObjectOfType<AgentChooser>();
        pointsManager = FindObjectOfType<PointsManager>();
        cowsManager = FindObjectOfType<CowsManager>();
        agentsInfoGetter = FindObjectOfType<AgentsInfoGetter>();
        aliensInfoGetter = FindObjectOfType<AliensInfoGetter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}