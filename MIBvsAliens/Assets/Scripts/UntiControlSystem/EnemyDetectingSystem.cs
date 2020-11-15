using System;
using DataStructures;
using UnityEngine;

public class EnemyDetectingSystem : MonoBehaviour
{
    private bool _initialized;

    private Race _race;

    public void Initialize(Race race)
    {
        if (_initialized)
            return;
        
        _race = race;
        TargetsQueue = new Queue<FightingSystem>();
        
        _initialized = true;
    }
    
    public Queue<FightingSystem> TargetsQueue { get; private set; }
    public event EventHandler EnemyDetected;
    public event EventHandler NoEnemiesAround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_initialized)
            return;

        if (other.TryGetComponent<BaseCreature>(out var otherCreature) &&
            other.TryGetComponent<FightingSystem>(out var otherFightingSystem) && 
            other.TryGetComponent<UnitControlSystem>(out var otherControlSystem)) 
        {
            if (otherCreature.race == _race)
                return;
            
            if (!otherFightingSystem.IsValidTarget)
                 return;

            otherFightingSystem.Died += EnemyDied;
            otherControlSystem.WillBeDestroyed += EnemyWillBeDestroyed;

            TargetsQueue.Enqueue(otherFightingSystem);
            OnEnemyDetected();
        }
    }

    private void EnemyWillBeDestroyed(object sender, EventArgs e)
    {
        if (sender is UnitControlSystem controlSystem)
        {
            RemoveEnemy(controlSystem);
        }
    }

    private void RemoveEnemy(UnitControlSystem controlSystem, FightingSystem fightingSystem = null)
    {
        controlSystem.WillBeDestroyed -= EnemyWillBeDestroyed;
        if (fightingSystem == null)
            fightingSystem = controlSystem.GetComponent<FightingSystem>();
        RemoveFightingSystemFromQueue(fightingSystem);
    }

    private void EnemyDied(object sender, EventArgs e)
    {
        if (sender is FightingSystem fightingSystem)
        {
            var controlSystem = fightingSystem.GetComponent<UnitControlSystem>();
            RemoveEnemy(controlSystem, fightingSystem);
        }
    }

    private void RemoveFightingSystemFromQueue(FightingSystem fightingSystem)
    {
        TargetsQueue.TryRemove(fightingSystem);
        fightingSystem.Died -= EnemyDied;
        if (TargetsQueue.IsEmpty())
            OnNoEnemiesAround();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_initialized)
            return;
        //is it even possible?
    }
    
    private void OnEnemyDetected()
    {
        EnemyDetected?.Invoke(this, EventArgs.Empty);
    }
    
    
    private void OnNoEnemiesAround()
    {
        NoEnemiesAround?.Invoke(this, EventArgs.Empty);
    }  

    public void Dispose()
    {
        FightingSystem fs = null;
        while (TargetsQueue.TryDequeue(out fs))
        {
            fs.Died -= EnemyDied;
        }
    }
}
