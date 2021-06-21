using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Audio;

public class InputDelegateBehaviour : MonoBehaviour
{
    private InputMaster _playerControls;
    private PlayerMovementBehaviour _playerMovement;
    [SerializeField]
    private List<BulletEmitterBehaviour> _regularEmitters;
    public List<BulletEmitterBehaviour> RegularEmitters { get { return _regularEmitters; } }
    [SerializeField]
    private List<BulletEmitterBehaviour> _tripleEmitters;
    public List<BulletEmitterBehaviour> TripleEmitters { get { return _tripleEmitters; } }

    [SerializeField]
    private PlayerManagerScriptable _playerManager;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private AudioSource _shootSound;

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
        _shootSound = GetComponent<AudioSource>();
        _shootSound.volume = 0.2f;

        _playerControls.Player.Fire.started += ctx => _isFireHold = true;
        _playerControls.Player.Fire.canceled += ctx => _isFireHold = false;

        _playerManager.BulletScale = 1;
        _playerManager.Damage = 1;
        _playerManager.FireForce = 15;
        _playerManager.RateOfFire = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //increment time
        _time += Time.deltaTime;
        //apply movement input to the movement script
        _playerMovement.Move(_playerControls.Player.move.ReadValue<Vector2>());
        playerAnimator.SetBool("isfireing", _isFireHold);
        if (_isFireHold && _time > _playerManager.RateOfFire)//if the input is held down and time is true
        {
            foreach (BulletEmitterBehaviour emitter in _regularEmitters)//for each regular emitter
            {
                emitter.Bullet.GetComponent<BulletBehaviour>().Damage = _playerManager.Damage;//apply damage value to the bullet
                emitter.Fire(emitter.transform.forward * _playerManager.FireForce, _playerManager.BulletScale);//apply the bullets movement
                _shootSound.PlayOneShot(_shootSound.clip);
            }
            foreach (BulletEmitterBehaviour emitter in _tripleEmitters)//for each Triple emitter
            {
                emitter.Bullet.GetComponent<BulletBehaviour>().Damage = _playerManager.Damage;//apply damage value to the bullet
                emitter.Fire(emitter.transform.forward * _playerManager.FireForce, _playerManager.BulletScale);//apply the bullets movement
            }
            _time = 0;//reset time
        }
    }
}
