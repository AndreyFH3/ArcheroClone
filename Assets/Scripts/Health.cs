using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public delegate void DoOnDeath();
public class Health : MonoBehaviour, IDamagable
{
    private protected int _health;
    private protected int _maxHealth;
    
    private protected UnityEvent _onDead = new UnityEvent();

    public void SetHealth(int health) { _maxHealth = _health = health;  }

    public bool GetDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            _onDead.Invoke();
            _onDead.RemoveAllListeners();
            return true;
        }
        return false;
    }

}
