using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerManager", menuName = "Manager/PlayerManager")]
public class PlayerManagerScriptable : ScriptableObject
{
    public float Damage;
    [Range(0.02f,10)]
    public float RateOfFire;
}
