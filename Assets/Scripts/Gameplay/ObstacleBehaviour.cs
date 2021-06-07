using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How much damage this obstacle will do.")]
    [SerializeField]
    private float _damage;
    [Tooltip("Holds the tag of the thing that shot it")]
    [SerializeField]
    private string _hostTag;

    public string Host
    {
        get { return _hostTag; }
        set { _hostTag = value; }
    }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Grab the health behaviour attached to the object
        HealthBehaviour health = collision.collider.GetComponent<HealthBehaviour>();
        if (!collision.collider.gameObject.CompareTag(_hostTag) && !collision.collider.gameObject.CompareTag("Obstacle") || !collision.collider.gameObject.CompareTag("Bullet"))
        {
            //If the health behaviour isn't null, deal damage
            if (health)
                health.TakeDamage(Damage);
            //destroys the obstacle
            Destroy(gameObject);
        }
    }
}
