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
    public GameObject CurrencyIcon;

    private void Start()
    {
        StartCoroutine(AutomaticallyIncrease(AutomaticPoints, AutomaticDelay));
    }

    public void AddPoints(object sender, EventArgs args)
    {
        if (sender is FightingSystem alien)
        {
            Debug.Log("Add points");

            var info = GameManager.instance.aliensInfoGetter.GetFor(alien.GetComponent<BaseAlien>().type);
            IncreasePoints(info.cost);
            alien.Died -= AddPoints;
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
        CurrencyIcon.GetComponent<AnimationScale>().StartAnimation();
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
