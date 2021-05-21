using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxPosZ = 16.5f;
    [SerializeField]
    private GameObject _telePos;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, _speed) * Time.deltaTime;
        if (transform.position.z < _maxPosZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, _telePos.transform.position.z);
    }
}
