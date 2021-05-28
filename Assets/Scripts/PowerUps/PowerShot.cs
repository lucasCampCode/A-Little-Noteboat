using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : PowerUp
{
    [SerializeField]
    private PlayerManagerScriptable _manager;
    [SerializeField]
    private float _bulletScaleRate = 1;
    [SerializeField]
    private float _damageIncreaseRate = 1;

    public override void StartUpgrade()
    {
        _manager.BulletScale += _bulletScaleRate;
        _manager.Damage += _damageIncreaseRate;
    }
    public override void EndUpgrade()
    {
        _manager.BulletScale -= _bulletScaleRate;
        _manager.Damage -= _damageIncreaseRate;
    }
}
