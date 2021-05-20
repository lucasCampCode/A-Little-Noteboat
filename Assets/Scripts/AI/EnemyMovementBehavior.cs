using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementBehavior : MonoBehaviour
{
    enum Progress { FIRSTLOOP = 1, WAIT = 2, SECONDLOOP = 3, EXIT = 4, DESTROY = 5 };

    private Rigidbody _rigidbody;

    [Tooltip("The location that the enemy moves around before going to the sit spot")]
    [SerializeField] private Transform _loop1;
    [Tooltip("The spot the enemy sits on before leaving the screen")]
    [SerializeField] private Transform _waitSpot;
    [Tooltip("The location that the enemy moves around before leaving the screen")]
    [SerializeField] private Transform _loop2;
    [Tooltip("The spot the enemy goes to exit the screen")]
    [SerializeField] private Transform _exitSpot;

    private NavMeshAgent _agent;

    private int _progress = (int)Progress.FIRSTLOOP;

    private float _timeToWait;
    private float _timeOnLoop1 = 0;
    private float _timeWaited = 0;
    private float _timeOnLoop2 = 0;
    private bool _isWaiting = false;

    /// <summary>
    /// Whether or not the enemy is waiting in the sit spot
    /// </summary>
    public bool IsWaiting
    {
        get { return _isWaiting; }
    }

    /// <summary>
    /// The location that the enemy moves around before going to the sit spot
    /// </summary>
    public Transform Loop1
    {
        get { return _loop1; }
        set { _loop1 = value; }
    }

    /// <summary>
    /// The spot the enemy waits on before leaving the screen
    /// </summary>
    public Transform WaitSpot
    {
        get { return _waitSpot; }
        set { _waitSpot = value; }
    }

    /// <summary>
    /// The location that the enemy moves around before leaving the screen
    /// </summary>
    public Transform Loop2
    {
        get { return _loop2; }
        set { _loop2 = value; }
    }

    /// <summary>
    /// The spot the enemy goes to exit the screen
    /// </summary>
    public Transform ExitSpot
    {
        get { return _exitSpot; }
        set { _exitSpot = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeToWait = Random.Range(5, 15);
        //Assign the rigidbody and agent variables
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(_progress)
        {
            case (int)Progress.FIRSTLOOP:
                DoLoop(_loop1, ref _timeOnLoop1);
                break;

            case (int)Progress.WAIT:
                _isWaiting = true;
                //If on the sit spot
                if (DistanceToTarget(WaitSpot) < 1)
                    //Add time to the time waited
                    _timeWaited += Time.deltaTime;
                //If time waited is or is greater than the random wait time
                if (_timeWaited >= _timeToWait)
                {
                    //Set isOnSitSpot to false
                    _isWaiting = false;
                    //Progress to the next stage
                    _progress++;
                    //Do a triple shot
                    _rigidbody.GetComponent<EnemyShootBehaviour>().tripleShot = true;
                }
                else
                    //Go straight to the second target
                    _agent.SetDestination(_waitSpot.position);
                break;

            case (int)Progress.SECONDLOOP:
                DoLoop(_loop2, ref _timeOnLoop2);
                break;

            case (int)Progress.EXIT:
                //If on the sit spot
                if (DistanceToTarget(_exitSpot) < 1)
                    //Progress to the next stage
                    _progress++;
                else
                    //Go straight to the second target
                    _agent.SetDestination(_exitSpot.position);
                break;

            case (int)Progress.DESTROY:
                Destroy(gameObject);
                break;
        }
    }

    /// <summary>
    /// Calculates the distance to the passed in transform
    /// </summary>
    /// <param name="target">The transform of the target</param>
    /// <returns>distance between the rigidbody's position and the target's position</returns>
    private float DistanceToTarget(Transform target)
    {
        return (transform.position - target.position).magnitude;
    }

    private void DoLoop(Transform loop, ref float timeOnLoop)
    {
        Vector3 toTarget = (loop.position - transform.position).normalized;

        //If too far
        if (DistanceToTarget(loop) > 4)
            //Move to the target
            _agent.SetDestination(loop.position);
        //If close enough
        else
        {
            timeOnLoop += Time.deltaTime;

            //If the amount of time that the agent has been orbiting the first target is greater than two seconds
            if (timeOnLoop > 2)
            {
                //Progress to the next stage
                _progress++;
                return;
            }
            //Turn sideways
            _agent.SetDestination(transform.position + toTarget.normalized + Vector3.Cross(toTarget, Vector3.up));
        }
    }
}