using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("the turrent that the mouse will me looking at")]
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _distanceFromCenter = 1;
    private float _tiltAmount = 0.5f;
    [SerializeField,Tooltip("how much the boat will tilt left and right")]
    private float _tilt = 30;
    [SerializeField]
    private float _tiltSpeed = 0.10f;
    private float _tiltReturnSpeed;

    private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _tiltReturnSpeed = _tiltSpeed / 2;
        // updates movement
        float xClamp = Mathf.Clamp(transform.position.x,-_distanceFromCenter,_distanceFromCenter);
        Vector3 nextPos = new Vector3(xClamp, transform.position.y, transform.position.z) + _velocity;
        _rigidbody.MovePosition(nextPos);

        Banking();
    }
    private void Banking() 
    {
        if (_tiltAmount > 0.5f)
            _tiltAmount -= _tiltReturnSpeed;
        else if (_tiltAmount < 0.5f)
            _tiltAmount += _tiltReturnSpeed;
        if (_velocity.x > 0)
            _tiltAmount += _tiltSpeed;
        else if(_velocity.x < 0)
            _tiltAmount -= _tiltSpeed;

        if (_tiltAmount > 0.5f - _tiltReturnSpeed && _tiltAmount < 0.5f + _tiltReturnSpeed)
            _tiltAmount = 0.5f;
        _tiltAmount = Mathf.Clamp(_tiltAmount, 0, 1);


        _rigidbody.rotation = Quaternion.Euler(0,0, Mathf.Lerp(-_tilt, _tilt, _tiltAmount));

    }

    /// <summary>
    /// updates the movement to the prefab
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        //moves the player on the x axis 
        _velocity = new Vector3(dir.x, 0, 0) * _speed * Time.deltaTime;
    }
}
