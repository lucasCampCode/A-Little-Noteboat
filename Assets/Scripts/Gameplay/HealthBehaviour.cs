using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private bool _destroyOnDeath;
    [SerializeField] private bool _isPlayer = false;
    private float _remainingInvincibilityTime = 0;

    private bool _tripedTrigger = false;

    public float Health
    {
        get { return _health; }
    }

    /// <summary>
    /// Subtracts the given damage value from health
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        //If this is the player and there is no invincibility time left
        if (_isPlayer && _remainingInvincibilityTime <= 0)
        {
            //Reset the invincibility time
            _remainingInvincibilityTime = 2;
        }
        //If there are invincibility frames left
        else if (_remainingInvincibilityTime > 0)
            return;
        _health -= damage;

        if (_health <= 0)
            _health = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Decrement the invincibility time
        _remainingInvincibilityTime -= Time.deltaTime;

        //If this character will be destroyed upon death and is dead, destroy the gameobject
        if (_destroyOnDeath && _health <= 0)
            Destroy(gameObject);
        //If this character will not be destroyed upon death and is dead
        else if (!_destroyOnDeath && _health <= 0)
        {
            if (!_tripedTrigger)
            {
                _animator?.SetTrigger("hasDied");
                _tripedTrigger = !_tripedTrigger;
            }
        }
    }
}
