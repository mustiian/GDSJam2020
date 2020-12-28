using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BasesController : MonoBehaviour
{
    [SerializeField]private int agentBasesCount = 0;
    [SerializeField]private int alienBasesCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in FindObjectsOfType<HealthBar>())
        {
            if (item.type == Race.Agents)
                agentBasesCount++;
            else
                alienBasesCount++;

            item.Destroyed += BaseOnDestroyed;
        }
    }

    private void BaseOnDestroyed(object sender, EventArgs e)
    {
        if (sender is HealthBar levelBase)
        {
            if (levelBase.type == Race.Aliens)
            {
                alienBasesCount--;
                if (alienBasesCount == 0)
                    LevelManager.instance.LevelWin();
            }
            else
            {
                agentBasesCount--;
                if (agentBasesCount == 0)
                    LevelManager.instance.LevelLose();
            }

            levelBase.fightingSystem.AfterAnimationDied -= BaseOnDestroyed;
        }
    }
}
