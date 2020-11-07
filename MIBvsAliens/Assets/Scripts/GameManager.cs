using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public AgentChooser agentChooser;
    [HideInInspector]public PointsManager pointsManager;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
