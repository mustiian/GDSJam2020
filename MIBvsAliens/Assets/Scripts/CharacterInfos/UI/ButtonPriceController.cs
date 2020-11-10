using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ButtonPriceController : MonoBehaviour
{
    public AgentType agentType;
    private int _price;
    private Button _button;
    private ButtonPriceController[] _allButtonControllers;
    private ColorBlock _defaultColors;

    private void Start()
    {
        var info = GameManager.instance.agentsInfoGetter.GetFor(agentType);
        var text = gameObject.GetComponent<Text>();
        text.text = info.cost.ToString();

        _price = info.cost;
        _button = GetComponentInParent<Button>();
        _button.onClick.AddListener(Call);
        _defaultColors = _button.colors;
        
        var panelTag = FindObjectOfType<PanelTag>();
        var buttons = panelTag.gameObject.GetComponentsInChildren<Button>();
        _allButtonControllers = buttons.Where(b => b.name.StartsWith("Agent"))
            .Select(b => b.GetComponentInChildren<ButtonPriceController>()).ToArray();
    }

    private void Call()
    {
        foreach (var controller in _allButtonControllers)
        {
            controller.Deselect();
        }
        Select();
    }

    private void FixedUpdate()
    {
        var available = GameManager.instance.pointsManager.HasRequiredPoints(_price);
        if (_button.interactable != available)
        {
            if (available)
                EnableButton();
            else
                DisableButton();
        }
    }

    private void DisableButton()
    {
        _button.interactable = false;
    }
    
    private void EnableButton()
    {
        _button.interactable = true;
    }

    private void Select()
    {
        var colors = _button.colors;
        colors.normalColor = new Color(_defaultColors.pressedColor.r, _defaultColors.pressedColor.g,
            _defaultColors.pressedColor.b, _defaultColors.pressedColor.a);
        _button.colors = colors;
    }

    private void Deselect()
    {
        _button.colors = _defaultColors;
    }
}
