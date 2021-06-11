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
    [SerializeField]
    private bool _piercingBullet;
    [SerializeField]
    private float _rotate;

    public bool PiercingBullet 
    { 
        get { return _piercingBullet; } 
        set { _piercingBullet = value; } 
    }
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

    private void Update()
    {
        transform.Rotate(_rotate, _rotate, _rotate);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Grab the health behaviour attached to the object
        HealthBehaviour health = other.GetComponent<HealthBehaviour>();
        if (!other.gameObject.CompareTag(_hostTag) && !other.gameObject.CompareTag("Bullet"))
        {
            if (_piercingBullet)
                PercingBulletAction(health);
            else
                RegularBulletAction(health);
        }
    }
    private void PercingBulletAction(HealthBehaviour health)
    {
        if (health)
            health.TakeDamage(1);
        _damage--;
        if (_damage <= 0)
        {
            //destroys the bullet
            Destroy(gameObject);
        }
    }
    private void RegularBulletAction(HealthBehaviour health)
    {

        if (health)
            health.TakeDamage(Damage);
        //destroys the bullet
        Destroy(gameObject);
    }
}
