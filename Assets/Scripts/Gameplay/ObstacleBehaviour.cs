using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How much damage this obstacle will do.")]
    [SerializeField]
    private float _damage;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Grab the health behaviour attached to the object
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();
        if (other.gameObject.CompareTag("Player"))
        {
            //If the health behaviour isn't null, deal damage
            if (health)
                health.TakeDamage(Damage);
            //destroys the obstacle
            Destroy(gameObject);
        }
    }
}
