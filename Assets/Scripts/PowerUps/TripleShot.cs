using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : PowerUp
{
    [SerializeField]
    private GameObject _player;
    public GameObject Player { set { _player = value; } }
    private InputDelegateBehaviour _delegate;
    public override void StartUpgrade()
    {
        _delegate = _player.GetComponent<InputDelegateBehaviour>();
        foreach(BulletEmitterBehaviour emitter in _delegate.RegularEmitters)
        {
            emitter.gameObject.SetActive(false);
        }
        foreach (BulletEmitterBehaviour emitter in _delegate.TripleEmitters)
        {
            emitter.gameObject.SetActive(true);
        }
    }
    public override void EndUpgrade()
    {
        _delegate = _player.GetComponent<InputDelegateBehaviour>();
        foreach (BulletEmitterBehaviour emitter in _delegate.RegularEmitters)
        {
            emitter.gameObject.SetActive(true);
        }
        foreach (BulletEmitterBehaviour emitter in _delegate.TripleEmitters)
        {
            emitter.gameObject.SetActive(false);
        }
    }

}
