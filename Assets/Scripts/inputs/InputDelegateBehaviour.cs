using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegateBehaviour : MonoBehaviour
{
    private InputMaster _playerControls;
    private PlayerMovementBehaviour _playerMovement;
    [SerializeField]
    private List<BulletEmitterBehaviour> _emitters;
    [SerializeField]
    private float _fireForce = 5;
    private void Awake()
    {
        _playerControls = new InputMaster();
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovementBehaviour>();

        foreach(BulletEmitterBehaviour emitter in _emitters)
            _playerControls.Player.Fire.performed += ctx => emitter.Fire(emitter.transform.forward * _fireForce);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerMovement.Move(_playerControls.Player.move.ReadValue<Vector2>());

        if (_playerControls.Player.Fire.ReadValue<bool>())
        {

        }
    }
}
