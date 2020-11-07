using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [HideInInspector]public int Points;
    
    public int AutomaticIncreasePoints;
    public float AutomaticIncreaseDelay;
    public TextMeshProUGUI UIText;

    private void Start()
    {
        StartCoroutine(AutomaticlyIncrease(AutomaticIncreasePoints, AutomaticIncreaseDelay));
    }

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

    private IEnumerator AutomaticlyIncrease(int value, float time)
    {
        while (true)
        {
            Points += AutomaticIncreasePoints;
            UIText.text = Points.ToString();
            yield return new WaitForSeconds(time);
        }
    }
}
