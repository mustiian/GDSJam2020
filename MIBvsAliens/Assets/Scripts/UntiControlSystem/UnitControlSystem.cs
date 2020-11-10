using System;
using UnityEngine;

public class CharacterInfo
{
    public int health;
    public int damage;
    public int speed;
}

public class UnitControlSystem : MonoBehaviour
{
    public float delayAfterDeath = 2; 
    
    enum State
    {
        Moving,
        Fighting
    }
    
    private EnemyDetectingSystem _detectingSystem;
    private FightingSystem _fightingSystem;
    private MovementSystem _movementSystem;

    private State _state;

    public void Initialize(Vector3 startPosition, Vector3 endPosition)
    {
        _detectingSystem = gameObject.GetComponent<EnemyDetectingSystem>();
        _fightingSystem = gameObject.GetComponent<FightingSystem>();
        _movementSystem = gameObject.GetComponent<MovementSystem>();
        
        
        var baseCreature = gameObject.GetComponent<BaseCreature>();
        CharacterInfo info;
        switch (baseCreature.race)
        {
            case Race.Agents:
            {
                var baseAgent = (BaseAgent) baseCreature;
                info = GameManager.instance.agentsInfoGetter.GetFor(baseAgent.type);
                break;
            }
            case Race.Aliens:
            {
                var baseAlien = (BaseAlien) baseCreature;
                info = GameManager.instance.aliensInfoGetter.GetFor(baseAlien.type);
                break;
            }
            default:
                throw new NotSupportedException($"Race {baseCreature.race:F} not supported");
        }
        
        var animator = gameObject.GetComponentInChildren<Animator>();

        _detectingSystem.Initialize(baseCreature.race);
        var queue = _detectingSystem.TargetsQueue;
        
        _fightingSystem.Initialize(queue, info.health, info.damage, animator);

        var transform = gameObject.GetComponent<Transform>();
        _movementSystem.Initialize(transform, startPosition, endPosition, info.speed, animator);
        
        _detectingSystem.EnemyDetected += DetectingSystemOnEnemyDetected;
        _detectingSystem.NoEnemiesAround += DetectingSystemOnNoEnemiesAround;
        _fightingSystem.AfterAnimationDied += FightingSystemOnAfterAnimationDied;

        _state = State.Moving;
        _movementSystem.Start();
    }
    
    private void FightingSystemOnAfterAnimationDied(object sender, EventArgs e)
    {
        _detectingSystem.Dispose();
        _detectingSystem.EnemyDetected -= DetectingSystemOnEnemyDetected;
        _detectingSystem.NoEnemiesAround -= DetectingSystemOnNoEnemiesAround;
        _fightingSystem.AfterAnimationDied -= FightingSystemOnAfterAnimationDied;
        
        Destroy(gameObject, delayAfterDeath);
    }

    private void DetectingSystemOnEnemyDetected(object sender, EventArgs e)
    {
        if (_state != State.Fighting)
        {
            _movementSystem.Stop();
            _state = State.Fighting;
            _fightingSystem.Start();
        }
    }

    private void DetectingSystemOnNoEnemiesAround(object sender, EventArgs e)
    {
        if (_state == State.Fighting)
        {
            _fightingSystem.Stop();
            _state = State.Moving;
            _movementSystem.Start();
        }
    }

    public void MoveTurnAround()
    {
        _movementSystem.TurnAround();
    }
}
