﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputDelegateBehaviour : MonoBehaviour
{
    private InputMaster _playerControls;
    private PlayerMovementBehaviour _playerMovement;
    [SerializeField]
    private List<BulletEmitterBehaviour> _emitters;
    [SerializeField]
    private float _fireForce = 5;
    [SerializeField]
    private PlayerManagerScriptable _playerManager;
    private float _time;
    private bool _isFireHold;
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

        _playerControls.Player.Fire.started += ctx => _isFireHold = true;
        _playerControls.Player.Fire.canceled += ctx => _isFireHold = false;
        //foreach (BulletEmitterBehaviour emitter in _emitters)
        //    _playerControls.Player.Fire.performed += ctx => emitter.Fire(emitter.transform.forward * _fireForce);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;
        _playerMovement.Move(_playerControls.Player.move.ReadValue<Vector2>());

        if (_isFireHold && _time > _playerManager.RateOfFire)
        {
            foreach (BulletEmitterBehaviour emitter in _emitters)
            {
                emitter.Bullet.GetComponent<BulletBehaviour>().Damage = _playerManager.Damage;
                emitter.Fire(emitter.transform.forward * _fireForce);
            }
            _time = 0;
        }
    }
}
