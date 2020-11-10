using System;
using System.Collections;
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
        if (sender is BaseAlien alien)
        {
            var info = GameManager.instance.aliensInfoGetter.GetFor(alien.type);
            IncreasePoints(info.cost);
            var fight = alien.GetComponent<FightingSystem>();
            fight.Died -= AddPoints;
            
            if (alien.HasCow)
            {
                alien.HasCow = false;
                GameManager.instance.cowsManager.DropCow();
            }
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
