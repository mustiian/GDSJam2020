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

    private void Start()
    {
        text.text = CowsNumber.ToString();
    }

    public void DecreaseCows(object sender, EventArgs args)
    {
        if (sender is BaseAlien alien)
        {
            if (alien.HasCow)
            {
                alien.HasCow = false;
                DropCow();
            }
        }
    }
    public void PickupCow()
    {
        if (CowsNumber > 1)
        {
            CowsNumber--;
        } else
        {
            CowsNumber--;
            text.text = CowsNumber.ToString();
            LevelManager.instance.CowsEnded(this);
        }
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
