using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootBehaviour : MonoBehaviour
{
    [SerializeField]
    private BulletEmitterBehaviour _emitterBehaviour;
    [SerializeField]
    private float _fireForce = 10;
    public void onFire(InputAction.CallbackContext ctx)
    {
        _emitterBehaviour.Fire(_emitterBehaviour.transform.forward * _fireForce);
    }
}
