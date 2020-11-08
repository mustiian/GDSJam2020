using System;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public event EventHandler Died;
    public event EventHandler AfterAnimationDied;
    
    public int damage = 1;
    public float attackSpeed = 1;
    
    private Health _health;
    private BaseCreature _fightingCreature;
    private Fight _currentEnemy;
    private readonly Queue<Fight> _enemiesToFight = new Queue<Fight>();
    
    void Start()
    {
        _health = gameObject.GetComponent<Health>();
        _fightingCreature = gameObject.GetComponent<BaseCreature>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherCreature = other.GetComponent<BaseCreature>();
        
        if (otherCreature == null)
            return;
        
        if (otherCreature.race == _fightingCreature.race)
            return;
        
        var otherFight = other.GetComponent<Fight>();
        _enemiesToFight.Enqueue(otherFight);
        if (_fightingCreature.state != State.Fighting)
        {
            ChangeToNextEnemy();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_enemiesToFight.Count == 0)
        {
            _fightingCreature.state = State.Moving;
        }
    }

    private void ChangeToNextEnemy()
    {
        if (_enemiesToFight.Count > 0)
        {
            _currentEnemy = _enemiesToFight.Dequeue();
            if (_currentEnemy.Alive())
            {
                _fightingCreature.state = State.Fighting;
                _fightingCreature.PlayAttackAnimation();
                Debug.Log(_fightingCreature.race.ToString("F") + "start fighting");
            }
                
            else
                ChangeToNextEnemy();
        }
        else
        {
            _fightingCreature.state = State.Moving;
            _fightingCreature.PlayMoveAnimation();
        }
    }

    private bool Alive()
    {
        return _health.current > 0;
    }

    private float _sinceLastAttack = 0;
    private void FixedUpdate()
    {
        _sinceLastAttack += Time.fixedDeltaTime;
        if (_sinceLastAttack < attackSpeed)
            return;
        
        if (_currentEnemy != null && _currentEnemy.Alive())
        {
            var damageToDeal = Time.fixedDeltaTime * damage;
            bool isDead = _currentEnemy.Hit(damageToDeal);
            _sinceLastAttack = 0;
            if (isDead)
            {
                //TODO: if player - get points. Somehow deal with multiple reward if more than 2 characters killed the enemy 
                ChangeToNextEnemy();
            }
        }
    }

    private bool Hit(float damageToDeal)
    {
        _health.current -= damageToDeal;
        if (!Alive())
        {
            _fightingCreature.state = State.Dying;
            _fightingCreature.PlayDeathAnimation();
            OnDied(EventArgs.Empty);
            return true;
        }

        return false;
    }
    
    protected void OnAfterAnimationDied(EventArgs e)
    {
        Debug.Log(_fightingCreature.race.ToString("F") + "is dead");
        EventHandler handler = Died;
        handler?.Invoke(_fightingCreature, e);
    }
    
    protected void OnDied(EventArgs e)
    {
        Debug.Log(_fightingCreature.race.ToString("F") + "is dead");
        EventHandler handler = Died;
        handler?.Invoke(_fightingCreature, e);
    }
}
