using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerManager", menuName = "Manager/PlayerManager")]
public class PlayerManagerScriptable : ScriptableObject
{
    public float Damage;
    public float RateOfFire;
    public float FireForce;
    public float BulletScale;
}
