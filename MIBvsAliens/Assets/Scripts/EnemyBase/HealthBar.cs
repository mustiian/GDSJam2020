using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private FightingSystem _fightingSystem;
    private UnitControlSystem _controlSystem;

    // Start is called before the first frame update
    void Start()
    {
        _controlSystem = GetComponent<UnitControlSystem>();
        _controlSystem.Initialize(Vector3.zero, Vector3.zero);
        _controlSystem.WillBeDestroyed += ControlSystemOnWillBeDestroyed;
        
        _fightingSystem = GetComponent<FightingSystem>();
        _fightingSystem.GotHit += FightSystemOnGotHit;
        _fightingSystem.AfterAnimationDied += FightSystemOnAfterAnimationDied;
    }

    private void FightSystemOnAfterAnimationDied(object sender, EventArgs e)
    {
        ReleaseEventHandlers();
    }

    private void FightSystemOnGotHit(object sender, float e)
    {
        
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
