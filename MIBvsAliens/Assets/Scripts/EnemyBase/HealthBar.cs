using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private FightingSystem _fightingSystem;
    private UnitControlSystem _controlSystem;
    private float maxHealth;
    private float health;
    private float spriteOriginalScaleX;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        _controlSystem = GetComponentInParent<UnitControlSystem>();
        _controlSystem.Initialize(Vector3.zero, Vector3.zero);
        _controlSystem.WillBeDestroyed += ControlSystemOnWillBeDestroyed;
        
        _fightingSystem = GetComponentInParent<FightingSystem>();
        _fightingSystem.GotHit += FightSystemOnGotHit;
        _fightingSystem.AfterAnimationDied += FightSystemOnAfterAnimationDied;

        var baseCreature = GetComponentInParent<BaseCreature>();
        sprite = GetComponent<SpriteRenderer>();
        if (baseCreature.race == Race.Aliens)
            maxHealth = GameManager.instance.charactersInfo.alienInfo[(int)AlienType.Building].characterInfo.health;
        else
            maxHealth = GameManager.instance.charactersInfo.alienInfo[(int)AgentType.Building].characterInfo.health;
        spriteOriginalScaleX = sprite.transform.localScale.x;
    }

    private void FightSystemOnAfterAnimationDied(object sender, EventArgs e)
    {
        ReleaseEventHandlers();
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
        _controlSystem.WillBeDestroyed -= ControlSystemOnWillBeDestroyed;
        _fightingSystem.GotHit -= FightSystemOnGotHit;
        _fightingSystem.AfterAnimationDied -= FightSystemOnAfterAnimationDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
