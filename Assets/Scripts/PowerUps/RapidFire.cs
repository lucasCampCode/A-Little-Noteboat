using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : PowerUp
{
    [SerializeField]
    private PlayerManagerScriptable _manager;
    [SerializeField]
    private float _newRateOfFire = 0.3f;
    [SerializeField]
    private float _newFireForce = 16;
    public override void StartUpgrade()
    {
        _manager.RateOfFire = _newRateOfFire;
        _manager.FireForce = _newFireForce;
    }
    public override void EndUpgrade()
    {
        _manager.RateOfFire = 0.5f;
        _manager.FireForce = 15;
    }

}
