using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : PowerUp
{
    [SerializeField]
    private PlayerManagerScriptable _manager;
    public override void StartUpgrade()
    {
        _manager.Damage = 2;
    }
    public override void EndUpgrade()
    {
        _manager.Damage = 1;
    }
}
