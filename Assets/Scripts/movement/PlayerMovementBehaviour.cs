using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("the turrent that the mouse will me looking at")]
    [SerializeField]
    private Transform _turrent;
    [Tooltip("the speed which the player will go")]
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private Camera _camera;

    private Vector2 _move;
    private Vector2 _look;
    /// <summary>
    /// when the user inputs a movement key
    /// </summary>
    /// <param name="ctx"></param>
    public void onMove(InputAction.CallbackContext ctx) 
    {
        _move = ctx.ReadValue<Vector2>();
    }
    /// <summary>
    /// grabs the location on where the player can move his turret 
    /// </summary>
    /// <param name="ctx"></param>
    public void onLook(InputAction.CallbackContext ctx)
    {
        _look = ctx.ReadValue<Vector2>();
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
        Look(_camera.ScreenPointToRay(_look));
    }
    /// <summary>
    /// updates the movement to the prefab
    /// </summary>
    /// <param name="dir"></param>
    void Move(Vector2 dir)
    {
        Vector3 move = new Vector3(dir.x, 0, dir.y) * _speed * Time.deltaTime;
        
        _rigidbody.MovePosition(transform.position + move);
    }
    /// <summary>
    /// updates what the turret will look at using the mouse position
    /// </summary>
    /// <param name="mouse"></param>
    void Look(Ray mouse) 
    {
        RaycastHit hit;
        if(Physics.Raycast(mouse,out hit))
        {
            _turrent.LookAt(new Vector3(hit.point.x, _turrent.position.y, hit.point.z));
        }
    }
}
