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
        Vector3 moveDirection = new Vector3();
        Vector3 velocity = new Vector3();
        //If the first loop is not complete
        if (!_firstLoopComplete)
        {
            Vector3 toWaitSpot = _waitSpot.position - transform.position;

            //If the distance to the waitSpot's position is greater than 2
            if ((transform.position - _waitSpot.position).magnitude > 2f)
            {
                //Calculate the direction and velocity towards the waitSpot
                moveDirection = (_waitSpot.position - transform.position).normalized;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move to the waitSpot
                _rigidbody.MovePosition(transform.position + velocity);
            }
            //If the distance to the waitSpot's position is less than 2
            else
            {
                //Calculate the direction perpendicular to the vector towards the waitSpot
                moveDirection = new Vector3(toWaitSpot.z, 0, -1 * toWaitSpot.x).normalized;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move sideways based on that vector
                _rigidbody.MovePosition(transform.position + velocity);

                //Increment time on first loop
                _timeOnFirstLoop += Time.deltaTime;
                //If the time on the first loop is greater than three seconds
                if (_timeOnFirstLoop > 3)
                    //Set the first loop to be complete
                    _firstLoopComplete = true;
            }
            //Look where the enemy is going
            transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
        }
        else if (!_waitComplete)
        {
            if ((transform.position - _waitSpot.position).magnitude > 0.25f)
            {
                //Calculate the direction and velocity towards the waitSpot
                moveDirection = (_waitSpot.position - transform.position).normalized;
                velocity = moveDirection * _moveSpeed * Time.deltaTime;

                //Move to the waitSpot
                _rigidbody.MovePosition(transform.position + velocity);
                //Look where the enemy is going
                transform.LookAt(new Vector3((transform.position + velocity).x, transform.position.y, (transform.position + velocity).z));
            }
            else
            {
                _timeWaiting += Time.deltaTime;
                //Look down the z axis
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1));
            }
        }

    }
}
