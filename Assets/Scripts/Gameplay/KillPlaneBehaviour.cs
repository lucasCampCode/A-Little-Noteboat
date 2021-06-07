using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the other object is has an "Enemy" or "Item" tag
        if (other.CompareTag("Enemy") || other.CompareTag("Item") || other.CompareTag("Obstacle"))
            //If true, the object is deleted
            Destroy(other.gameObject);
    }
}
