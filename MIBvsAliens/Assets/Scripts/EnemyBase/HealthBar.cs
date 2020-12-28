using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [HideInInspector]public FightingSystem fightingSystem;
    [HideInInspector]public UnitControlSystem controlSystem;
    [HideInInspector]public Race type;
    public event EventHandler Destroyed;

    private float maxHealth;
    private float spriteOriginalScaleX;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        controlSystem = GetComponentInParent<UnitControlSystem>();
        controlSystem.Initialize(Vector3.zero, Vector3.zero);
        controlSystem.WillBeDestroyed += ControlSystemOnWillBeDestroyed;
        
        fightingSystem = GetComponentInParent<FightingSystem>();
        fightingSystem.GotHit += FightSystemOnGotHit;
        fightingSystem.AfterAnimationDied += FightSystemOnAfterAnimationDied;

        var baseCreature = GetComponentInParent<BaseCreature>();
        sprite = GetComponent<SpriteRenderer>();
        type = baseCreature.race;

        if (type == Race.Aliens)
            maxHealth = GameManager.instance.charactersInfo.alienInfo[(int)AlienType.Building].characterInfo.health;
        else
            maxHealth = GameManager.instance.charactersInfo.agentInfo[(int)AgentType.Building].characterInfo.health;
        spriteOriginalScaleX = sprite.transform.localScale.x;
    }

    private void FightSystemOnAfterAnimationDied(object sender, EventArgs e)
    {
        ReleaseEventHandlers();
        Destroyed?.Invoke(this, EventArgs.Empty);
    }

    private void FightSystemOnGotHit(object sender, float e)
    {
        sprite.transform.localScale = new Vector3(spriteOriginalScaleX * (e / maxHealth), sprite.transform.localScale.y, sprite.transform.localScale.z);
    }

    private void ControlSystemOnWillBeDestroyed(object sender, EventArgs e)
    {
        ReleaseEventHandlers();
    }

    private void ReleaseEventHandlers()
    {
        controlSystem.WillBeDestroyed -= ControlSystemOnWillBeDestroyed;
        fightingSystem.GotHit -= FightSystemOnGotHit;
        fightingSystem.AfterAnimationDied -= FightSystemOnAfterAnimationDied;
    }
}
