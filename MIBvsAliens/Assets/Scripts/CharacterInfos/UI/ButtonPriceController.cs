using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPriceController : MonoBehaviour
{
    public AgentType agentType;
    private int _price;
    private Button _button;
    private void Start()
    {
        var info = GameManager.instance.agentsInfoGetter.GetFor(agentType);
        var text = gameObject.GetComponent<Text>();
        text.text = info.cost.ToString();

        _price = info.cost;
        _button = GetComponentInParent<Button>();
    }

    private void FixedUpdate()
    {
        var available = GameManager.instance.pointsManager.HasRequiredPoints(_price);
        if (_button.enabled != available)
        {
            if (available)
                EnableButton();
            else
                DisableButton();
        }
    }

    private void DisableButton()
    {
        _button.enabled = false;
    }
    
    private void EnableButton()
    {
        _button.enabled = true;
    }
}
