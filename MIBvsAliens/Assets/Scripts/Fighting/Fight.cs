﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public event EventHandler Died;
    
    public int damage = 1;
    
    private Health _health;
    private BaseCreature _fightingCreature;
    private Fight _currentEnemy;
    private readonly Queue<Fight> _enemiesToFight = new Queue<Fight>();
    void Start()
    {
        _health = gameObject.GetComponent<Health>();
        _fightingCreature = gameObject.GetComponent<BaseCreature>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherCreature = other.GetComponent<BaseCreature>();
        if (otherCreature.race == _fightingCreature.race)
            return;
        
        var otherFight = other.GetComponent<Fight>();
        _enemiesToFight.Enqueue(otherFight);
        if (_fightingCreature.state != State.Fighting)
        {
            ChangeToNextEnemy();
        }
    }

    private void ChangeToNextEnemy()
    {
        if (_enemiesToFight.Count > 0)
        {
            _currentEnemy = _enemiesToFight.Dequeue();
            if (_currentEnemy.Alive())
                _fightingCreature.state = State.Fighting;
            else
                ChangeToNextEnemy();
        }
        else
        {
            _fightingCreature.state = State.Moving;
        }
    }

    private bool Alive()
    {
        return _health.current > 0;
    }

    private void FixedUpdate()
    {
        if (_currentEnemy != null)
        {
            var damageToDeal = Time.fixedDeltaTime * damage;
            bool isDead = _currentEnemy.Hit(damageToDeal);
            if (isDead)
            {
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
            return true;
        }

        return false;
    }
    
    protected virtual void OnDied(EventArgs e)
    {
        EventHandler handler = Died;
        handler?.Invoke(this, e);
    }
}
