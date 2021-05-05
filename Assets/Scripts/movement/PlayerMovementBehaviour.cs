using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _turrent;
    [SerializeField]
    private float _speed = 1;

    private Vector2 _move;

    public void onMove(InputAction.CallbackContext ctx) 
    {
        _move = ctx.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(_move);
    }
    void Move(Vector2 dir)
    {
        Vector3 move = new Vector3(dir.x, 0, dir.y) * _speed * Time.deltaTime;
        
        _rigidbody.MovePosition(transform.position + move);
    }
}
