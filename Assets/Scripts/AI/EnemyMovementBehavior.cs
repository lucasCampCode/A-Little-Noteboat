using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _target1;
    [SerializeField] private GameObject _target2;
    private NavMeshAgent _agent;

    private bool _target1Found = false;
    private bool _target2Found = false;
    private float _timeOnFirst = 0;

    public GameObject Target2
    {
        get { return _target2; }
        set { _target2 = value; }
    }

    public GameObject Target1
    {
        get { return _target1; }
        set { _target1 = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Assign the rigidbody and agent variables
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = new Vector3();
        Vector3 direction = new Vector3();

        float currentDistance = (transform.position - _target1.transform.position).magnitude;

        //If the enemy hasn't gotten to the first target yet
        if (!_target1Found)
        {
            toTarget = (_target1.transform.position - transform.position).normalized;

            //If too far
            if (currentDistance > 5)
            {
                //Move to the target
                _agent.SetDestination(_target1.transform.position);
            }
            else
            {
                _timeOnFirst += Time.deltaTime;

                //If the amount of time that the agent has been orbiting the first target is greater than two seconds
                if (_timeOnFirst > 2)
                {
                    //Set the found variable to true
                    _target1Found = true;
                    return;
                }
                //Turn sideways
                direction = new Vector3(toTarget.y, toTarget.x * -1, toTarget.z);
                _agent.SetDestination(transform.position + toTarget.normalized + Vector3.Cross(toTarget, Vector3.up));
                return;
            }
        }
        //If the first target has been found and the second target hasn't been found
        else if (!_target2Found)
        {
            //Go straight to the second target
            _agent.SetDestination(_target2.transform.position);
        }
    }
}
