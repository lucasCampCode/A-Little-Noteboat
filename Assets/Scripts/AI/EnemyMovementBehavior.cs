using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("The target of the EnemyShootBehavior")]
    [SerializeField] private GameObject _player;

    [SerializeField] private float _moveSpeed;

    Vector3 velocity;

    private bool _firstLoopComplete = false;
    private bool _waitComplete = false;
    private bool _secondLoopComplete = false;
    private float _timeOnFirstLoop = 0;
    private float _timeWaiting = 0;
    private float _timeOnSecondLoop = 0;

    private bool _isLooping = false;

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

    public bool IsWaiting { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 toWaitSpot = (_waitSpot.position - transform.position).normalized;
        Vector3 moveDirection = new Vector3();
        Vector3 desiredVelocity = new Vector3();
        Vector3 steeringForce = new Vector3();

        //If the first loop is not complete
        if (!_firstLoopComplete)
        {
            //If the distance to the waitSpot's position is greater than 1 and isn't looping
            if ((transform.position - _waitSpot.position).magnitude > 1f && !_isLooping)
            {
                //Calculate the steering force and velocity
                desiredVelocity = toWaitSpot * _moveSpeed;
                steeringForce = desiredVelocity - velocity;
                velocity += steeringForce;

                //Move to the waitSpot
                _rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
            }
            //If the enemy is looping 
            else
            {
                //Set isLooping to true to prevent snapping the forwards to be towards the waitSpot while looping
                _isLooping = true;

                //Calculate the direction perpendicular to the vector towards the waitSpot
                moveDirection = new Vector3(toWaitSpot.z, 0, -1 * toWaitSpot.x).normalized;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move sideways based on that vector
                _rigidbody.MovePosition(transform.position + velocity);

                //Increment time on first loop
                _timeOnFirstLoop += Time.deltaTime;
                //If the time on the first loop is greater than 2.5 seconds
                if (_timeOnFirstLoop > 2.5)
                {
                    //Set the first loop to be complete
                    _firstLoopComplete = true;
                    _isLooping = false;
                }
            }
            //Look where the enemy is going
            transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
        }
        //If the enemy has not waited
        else if (!_waitComplete)
        {
            //If not on the wait spot
            if ((transform.position - _waitSpot.position).magnitude > 0.25f)
            {
                //Calculate the steering force and velocity
                desiredVelocity = toWaitSpot * _moveSpeed;
                steeringForce = desiredVelocity - velocity;
                velocity += steeringForce;

                //Move to the waitSpot
                _rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);

                //Look where the enemy is going
                transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
                //transform.forward = Vector3.Lerp(transform.forward, toWaitSpot, 0.25f);
            }
            //If on the wait spot
            else
            {
                IsWaiting = true;

                //Look down the z axis
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1));

                //Increment time waiting
                _timeWaiting += Time.deltaTime;
                if (_timeWaiting > 5)
                {
                    _waitComplete = true;
                    IsWaiting = false;
                }
            }
        }
        //If the enemy has not done the second loop
        else if (!_secondLoopComplete)
        {
            //Calculate the direction perpendicular to the vector towards the waitSpot
            moveDirection = new Vector3(toWaitSpot.z, 0, -1 * toWaitSpot.x).normalized;
            velocity = moveDirection * _moveSpeed * Time.deltaTime;

            //Move sideways based on that vector
            _rigidbody.MovePosition(transform.position + velocity);

            //Increment time on first loop
            _timeOnSecondLoop += Time.deltaTime;
            //If the time on the first loop is greater than three seconds
            if (_timeOnSecondLoop > 1)
                //Set the first loop to be complete
                _secondLoopComplete = true;

            //Look where the enemy is going
            transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
        }
        //If the enemy has not exited
        else
        {
            //If not on the exit spot
            if ((transform.position - _exitSpot.position).magnitude > 0.25f)
            {
                //Calculate the direction and velocity towards the exitSpot
                moveDirection = (_exitSpot.position - transform.position).normalized;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move to the exit spot
                _rigidbody.MovePosition(transform.position + velocity);
            }
            //If on the exit spot
            else
            {
                //Destroy the enemy this script is attached to
                Destroy(gameObject);
            }
            //Look where the enemy is going
            transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
        }
    }
}
