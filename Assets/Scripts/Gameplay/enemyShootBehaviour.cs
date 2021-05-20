using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private BulletEmitterBehaviour _bulletEmitter;
    private EnemyMovementBehavior _movement;
    [Tooltip("how fast the bullet will travel")]
    [SerializeField]
    private float _speed = 5;
    [Tooltip("how long it takes to shoot a the target")]
    [SerializeField]
    private float _timePerShoot = 2;
    [Range(-1, 1)]
    [Tooltip("how much can the enemy see the target")]
    [SerializeField]
    private float _sightRange = 0.5f;

    private float _time;
    private int _shotsFired = 0;

    public bool tripleShot = false;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }
    private void Start()
    {
        _movement = GetComponent<EnemyMovementBehavior>();
    }

    void FixedUpdate()
    {

        //gets the direction to the target
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        //calculates the angle from the direction to the items forward vector
        float attackAngle = Vector3.Dot(direction, transform.forward);
        //if the angle of attack is in sight of the target
        if (_sightRange < attackAngle)
        {
            //increase time to shoot by the time per frame
            _time += Time.deltaTime;
            if (!_movement.IsWaiting)
            {
                //when shots fired is greater than two 
                if (_shotsFired >= 2 && _time > _timePerShoot)
                {
                    //turn off triple shot
                    tripleShot = false;
                    //reset shots fired
                    _shotsFired = 0;
                }
                //if tripleshot is true 
                if (tripleShot && _time > _timePerShoot)
                {
                    SetTimeOfFire(direction, _timePerShoot / 1.1f);
                    //add to shot fired
                    _shotsFired++;
                }
                else if (!tripleShot && _time > _timePerShoot)
                    SetTimeOfFire(direction, 0);
            }
        }
    }

    void SetTimeOfFire(Vector3 dir, float time)
    {
        _bulletEmitter.Fire(dir * _speed);
        //reset time to shoot to 0
        _time = time;
    }
}

