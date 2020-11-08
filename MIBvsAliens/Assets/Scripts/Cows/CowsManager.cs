using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CowsManager : MonoBehaviour
{
    public int CowsNumber;
    public TextMeshProUGUI text;

    public void PickupCow()
    {
        if (CowsNumber > 0)
        {
            CowsNumber--;
        } else
        {
            LevelManager.instance.CowsEnded(this);
        }
        text.text = CowsNumber.ToString();
    }

    public void DropCow()
    {
        CowsNumber++;
        text.text = CowsNumber.ToString();
    }
}
