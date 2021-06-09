using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnYAxisBehaviour : MonoBehaviour
{
    float rotateY = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateY += 15 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,rotateY,0);
    }
}
