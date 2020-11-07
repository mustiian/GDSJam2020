using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [HideInInspector]public int Points;

    public int AutomaticIncreasePoints;
    public int AutomaticIncreaseDelay;

    private void Start()
    {
        StartCoroutine(AutomaticlyIncrease(AutomaticIncreasePoints, AutomaticIncreaseDelay));
    }

    public void ReducePoints(int value)
    {
        Points -= value;
        if (Points < 0)
            Points = 0;
    }

    public void IncreasePoints(int value)
    {
        Points += value;
    }

    private IEnumerator AutomaticlyIncrease(int value, int time)
    {
        while (true)
        {
            Points += AutomaticIncreasePoints;
            yield return new WaitForSeconds(time);
        }
    }
}
