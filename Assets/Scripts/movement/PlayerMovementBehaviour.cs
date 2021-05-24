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

    private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // updates movement
        _rigidbody.MovePosition(transform.position + _velocity);
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
