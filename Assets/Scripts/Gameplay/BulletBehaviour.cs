using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Tooltip("How much damage this bullet will do.")]
    [SerializeField]
    private float _damage;
    [Tooltip("The amount of time it takes for this bullet to despawn after being fired.")]
    [SerializeField]
    private float _despawnTime;
    [SerializeField]
    private string _hostTag;
    public string Host
    {
        get { return _hostTag; }
        set { _hostTag = value; }
    }
    public Rigidbody Rigidbody
    {
        get{ return _rigidbody; }
    }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Destroy this object once after enough time has passed
        Destroy(gameObject, _despawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Grab the health behaviour attached to the object
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();
        if (!other.gameObject.CompareTag(_hostTag) && !other.gameObject.CompareTag("Bullet"))
        {
            //If the health behaviour isn't null, deal damage
            if (health)
                health.TakeDamage(Damage);
            //destroies the bullet
            Destroy(gameObject);
        }
    }
}
