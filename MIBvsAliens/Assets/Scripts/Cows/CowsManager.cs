using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CowsManager : MonoBehaviour
{
    public int CowsNumber;
    public Text text;

    public GameObject CowPrefab;

    private int realCowsNumber;

    private void Start()
    {
        realCowsNumber = CowsNumber;
        text.text = CowsNumber.ToString();
    }

    public bool HasFreeCow()
    {
        return CowsNumber > 0;
    }

    public void DecreaseCows()
    {
        if (realCowsNumber > 1)
        {
            realCowsNumber--;
        }
        else
        {
            realCowsNumber--;
            text.text = CowsNumber.ToString();
            LevelManager.instance.LevelLose();
        }
    }
    public void PickupCow()
    {
        if (CowsNumber > 0)
            CowsNumber--;
        text.text = CowsNumber.ToString();
    }

    public void DropCow()
    {
        CowsNumber++;
        text.text = CowsNumber.ToString();
    }

    private void FixedUpdate()
    {
    }
}
