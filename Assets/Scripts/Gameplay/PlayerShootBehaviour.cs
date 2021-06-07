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
    /// <summary>
    /// when the user uses a fire key it fires a bullet
    /// </summary>
    /// <param name="ctx"></param>
    public void onFire(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            _emitterBehaviour.Fire(_emitterBehaviour.transform.forward * _fireForce);
    }
}
