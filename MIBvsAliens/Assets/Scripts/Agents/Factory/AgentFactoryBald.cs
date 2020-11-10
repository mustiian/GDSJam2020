﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFactoryBald : MonoBehaviour, IAgentFactory
{
    public GameObject[] prefab;

    public BaseAgent Create(Vector3 position, Vector3 endPosition)
    {
        var info = GameManager.instance.agentsInfoGetter.GetFor(AgentType.Bald);
        if (GameManager.instance.pointsManager.HasRequiredPoints(info.cost))
        {
            int index = Random.Range(0, prefab.Length);

            var gameObject = Instantiate(prefab[index], position, Quaternion.identity);
            var controlSystem = gameObject.GetComponent<UnitControlSystem>();
            controlSystem.Initialize(position, endPosition);
            return gameObject.GetComponent<BaseAgent>();
        }

        return null;
    }
}
