using System;
using UnityEngine;

public class UnitControlSystem : MonoBehaviour
{
    enum State
    {
        Moving,
        Fighting,
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
        _movementSystem.Enable();
    }
    
    public float delayAfterDeath = 2;
    public event EventHandler WillBeDestroyed;
    
    public void MoveTurnAround()
    {
        _movementSystem.TurnAround();
    }

    #region ControlMethods
    
    public void ChangeMovingSpeed(int speed)
    {
        _movementSystem.speed = speed;
    }

    public int GetMovingSpeed()
    {
        return _movementSystem.speed;
    }

    public void RequestDestroy()
    {
        _fightingSystem.IsValidTarget = false;
        OnWillBeDestroyed();
        DestroyUnit();
    }
    
    #endregion

    private void FightingSystemOnAfterAnimationDied(object sender, EventArgs e)
    {
        DestroyUnit();
    }

    private void DestroyUnit()
    {
        _detectingSystem.Dispose();
        _detectingSystem.EnemyDetected -= DetectingSystemOnEnemyDetected;
        _detectingSystem.NoEnemiesAround -= DetectingSystemOnNoEnemiesAround;
        _fightingSystem.AfterAnimationDied -= FightingSystemOnAfterAnimationDied;

        Destroy(gameObject, delayAfterDeath);
    }

    private void OnWillBeDestroyed()
    {
        WillBeDestroyed?.Invoke(this, EventArgs.Empty);
    }

    private void DetectingSystemOnEnemyDetected(object sender, EventArgs e)
    {
        if (_state != State.Fighting)
        {
            _movementSystem.Disable();
            _state = State.Fighting;
            _fightingSystem.Enable();
        }
    }

    private void DetectingSystemOnNoEnemiesAround(object sender, EventArgs e)
    {
        if (_state == State.Fighting)
        {
            _fightingSystem.Disable();
            _state = State.Moving;
            _movementSystem.Enable();
        }
    }
}
