using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : PowerUp
{
    [SerializeField]
    private PlayerManagerScriptable _manager;
    [SerializeField]
    [Tooltip("the amount that the rate of fire will change")]
    private float _rateOfFireAmount = -0.1f;
    [SerializeField]
    [Tooltip("the amount that the fire forece will change")]
    private float _FireForceAmount = 1;
    public override void StartUpgrade()
    {
        _manager.RateOfFire += _rateOfFireAmount;
        _manager.FireForce += _FireForceAmount;
    }
    public override void EndUpgrade()
    {
        _manager.RateOfFire -= _rateOfFireAmount;
        _manager.FireForce -= _FireForceAmount;
    }

}
