using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [Tooltip("The max z distance the object can move to")]
    [SerializeField]
    private float _maxPosZ;
    [Tooltip("The position the object will teleport to")]
    [SerializeField]
    private float _telePosZ;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, _speed) * Time.deltaTime;
        //Checks if the objects position goes past the maximum z
        if (transform.position.z < _maxPosZ)
            //If true, the object is teleported to the given _telePos's z position
            transform.position = new Vector3(transform.position.x, transform.position.y, _telePosZ);
    }
    public void setSpeed(float value)
    {
        _speed = value;
    }
}
