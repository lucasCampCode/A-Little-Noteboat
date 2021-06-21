using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScrollingBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed;
    private float _timeTillScroll;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 120 && _timeTillScroll > 3)
            transform.position += new Vector3(0, _scrollSpeed, 0) * Time.deltaTime;
        else
            _timeTillScroll += Time.deltaTime;
    }
}
