using System;
using DataStructures;
using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    enum FightingState
    {
        Idle,
        Attacking,
        Dying
    }

    private bool _initialized = false;
    
    private Queue<FightingSystem> _targetsQueue;
    private FightingSystem _currentTarget;
    private FightingState _state;
    private int _health;
    private int _damage;
    private Animator _animator;

    private bool _isFighting = false;

    public void Initialize(Queue<FightingSystem> targetQueue, int health,
        int damage, Animator animator)
    {
        if (_initialized)
            return;
        
        _targetsQueue = targetQueue;
        _health = health;
        _damage = damage;
        _state = FightingState.Idle;
        _animator = animator;
        
        _initialized = true;
    }

    public Boolean IsValidTarget
    {
        get => _state != FightingState.Dying;
        set => _state = value ? FightingState.Idle : FightingState.Dying;
    }

    public event EventHandler Died;
    public event EventHandler AfterAnimationDied;
    public event EventHandler<float> GotHit;

    public void Enable()
    {
        _isFighting = true;
    }

    public void Disable()
    {
        _isFighting = false;
    }
    
    void FixedUpdate()
    {
        if (!_initialized)
            return;
        
        if (!_isFighting)
            return;

        if (_state != FightingState.Idle)
            return;

        if (_currentTarget != null)
        {
            if (_currentTarget._state == FightingState.Dying)
                _currentTarget = null;
        }

        if (_currentTarget == null)
        {
            if (!_targetsQueue.TryDequeue(out _currentTarget))
                return;
        }
        
        StartHitting();
    }

    private void StartHitting()
    {
        Debug.Log("Attack started" + Time.time);
        _state = FightingState.Attacking;
        _animator.Play("Attack", -1, 0f);
        _animator.Update(Time.deltaTime);
        var currentAnimatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        var delay = currentAnimatorClipInfo[0].clip.length;
        Debug.Log("Animation length: " + delay);
        Invoke(nameof(EndHitting), delay);
    }

    private void EndHitting()
    {
        Debug.Log("Attack ended" + Time.time);
        if (_currentTarget == null)
            return;

        if (!Alive())
            return;
        
        _currentTarget.GetHit(_damage);
        _state = FightingState.Idle;
    }

    private void GetHit(int damage)
    {
        if (_state == FightingState.Dying)
            return;
        
        _health -= damage;
        OnGetHit();
        if (!Alive())
        {
            _state = FightingState.Dying;
            _animator.Play("Death");
            var currentAnimatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            var delay = currentAnimatorClipInfo[0].clip.length;
            OnDied();
            Invoke(nameof(OnAfterAnimationDied), delay);
        }
    }

    private bool Alive()
    {
        return _health > 0;
    }
    
    private void OnGetHit()
    {
        GotHit?.Invoke(this, _health);
    }

    private void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
    }

    private void OnAfterAnimationDied()
    {
        AfterAnimationDied?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        AfterAnimationDied?.Invoke(this, EventArgs.Empty);
    }
}
