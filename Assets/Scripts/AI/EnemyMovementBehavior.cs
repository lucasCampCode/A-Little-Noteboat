using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("The target of the EnemyShootBehavior")]
    [SerializeField] private GameObject _player;

    [SerializeField] private float _moveSpeed = 1;

    private bool _firstLoopComplete = false;
    private bool _waitComplete = false;
    private bool _secondLoopComplete = false;

    private bool _isWaiting = false;

    private float _timeOnFirstLoop = 0;
    private float _timeWaiting = 0;
    private float _timeOnSecondLoop = 0;

    [Tooltip("The spot that the enemy will orbit and wait at")]
    [SerializeField] private Transform _waitSpot;
    [Tooltip("The spot that the enemy will exit at")]
    [SerializeField] private Transform _exitSpot;

    public Transform WaitSpot
    {
        get { return _waitSpot; }
        set { _waitSpot = value; }
    }

    public Transform ExitSpot
    {
        get { return _exitSpot; }
        set { _exitSpot = value; }
    }

    public bool IsWaiting
    {
        get { return _isWaiting; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the first loop is not complete
        if (!_firstLoopComplete)
        {
            Vector3 moveDirection = new Vector3();
            Vector3 velocity = new Vector3();
            //If the distance between the current position and the waitSpot's position is less than 3
            if ((transform.position - _waitSpot.position).magnitude > 3f)
            {
                //Calculate the direction and velocity towards the waitSpot
                moveDirection = _waitSpot.position - transform.position;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move to and look at the waitSpot
                _rigidbody.MovePosition(transform.position + velocity);
                transform.LookAt(moveDirection);
            }
            else
            {
                //Calculate the direction and velocity towards the waitSpot
                moveDirection = transform.position - _waitSpot.position;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move sideways based on that vector
                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection.normalized + Vector3.Cross(moveDirection, Vector3.up), 1f * Time.deltaTime);

                //Increment time on first loop
                _timeOnFirstLoop += Time.deltaTime;
                //If the time on the first loop is greater than three seconds
                if (_timeOnFirstLoop > 3)
                    _firstLoopComplete = true;
            }
            transform.LookAt(moveDirection);
        }

    }
}
