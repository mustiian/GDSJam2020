using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public int Points;
    
    public int AutomaticPoints;
    public float AutomaticDelay;
    public Text UIText;

    private void Start()
    {
        StartCoroutine(AutomaticallyIncrease(AutomaticPoints, AutomaticDelay));
    }

    public void AddPoints(object sender, EventArgs args)
    {
        if (sender is BaseCreature creature)
        {
            IncreasePoints(creature.cost);
            var fight = creature.GetComponent<Fight>();
            fight.Died -= AddPoints;
        }
    }

    public bool HasRequiredPoints(int value) => Points - value >= 0 ? true : false;

    public void ReducePoints(int value)
    {
        Points -= value;
        if (Points < 0)
            Points = 0;

        UIText.text = Points.ToString();
    }

    public void IncreasePoints(int value)
    {
        Points += value;
        UIText.text = Points.ToString();
    }

    private IEnumerator AutomaticallyIncrease(int value, float time)
    {
        while (true)
        {
            Points += AutomaticPoints;
            UIText.text = Points.ToString();
            yield return new WaitForSeconds(time);
        }
    }
}
