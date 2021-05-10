using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, _moveSpeed) * Time.deltaTime;
    }
}
