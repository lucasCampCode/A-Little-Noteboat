﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShootBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private BulletEmitterBehaviour _bulletEmitter;
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _timePerShoot = 2;
    [SerializeField]
    private float _angleOfAttack = 0.5f;
    private float _time;
    public bool tripleShot = false;
    private int _shotsFired = 0;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    void FixedUpdate()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        float attackAngle = Vector3.Dot(direction, transform.forward);
        if (_angleOfAttack < attackAngle)
        {
            _time += Time.deltaTime;
            if (_time > _timePerShoot)
            {
                if (_shotsFired >= 2)
                {
                    tripleShot = false;
                    _shotsFired = 0;
                }
                if (tripleShot)
                {
                    _bulletEmitter.Fire(direction * _speed);
                    _time = _timePerShoot / 1.1f;
                    _shotsFired++;
                }
                else
                {
                    _bulletEmitter.Fire(direction * _speed);
                    _time = 0;
                }
            }
        }
    }
}
