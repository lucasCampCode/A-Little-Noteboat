using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 5;
    protected float _time;
    private bool _Started = false;
    /// <summary>
    /// what the upgrade should do at the start
    /// </summary>
    public abstract void StartUpgrade();
    /// <summary>
    /// what the upgrade should do when it ends
    /// </summary>
    public abstract void EndUpgrade();
    public virtual void Upgrade()
    {
        _time += Time.deltaTime;
        if (_time < _lifetime && !_Started)
        {
            StartUpgrade();
            _Started = true;
        }
        else if (_time > _lifetime && _Started)
        {
            EndUpgrade();
            Destroy(gameObject);
        }
    }
}
