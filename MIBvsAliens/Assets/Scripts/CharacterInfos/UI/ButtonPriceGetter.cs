using UnityEngine;
using UnityEngine.UI;

public class ButtonPriceGetter : MonoBehaviour
{
    public AgentType agentType;
    private void Start()
    {
        var info = GameManager.instance.agentsInfoGetter.GetFor(agentType);
        var text = gameObject.GetComponent<Text>();
        text.text = info.cost.ToString();
    }
}
