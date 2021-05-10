using System.Collections;
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

    private float _time;

    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    void FixedUpdate()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;

        _time += Time.deltaTime;
        if (_time > _timePerShoot)
        {
            _bulletEmitter.Fire(direction * _speed);
            _time = 0;
        }
    }
}
