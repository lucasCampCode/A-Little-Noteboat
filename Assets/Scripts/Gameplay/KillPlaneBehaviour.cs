using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Item"))
            Destroy(other.gameObject);
    }
}
