using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _items;
    private HealthBehaviour _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    public void DropRandomItem()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(_health.Health <= 0)
        {
            int rng = Random.Range(0, _items.Length);
            if (_items[rng])
            {
                GameObject item = Instantiate(_items[rng], transform.position, new Quaternion());
                TripleShot shot = item.GetComponent<TripleShot>();
                if (shot)
                    shot.Player = gameObject.GetComponent<EnemyShootBehaviour>().Target;
                Destroy(item, 30);
            }
            Destroy(gameObject);
        }
    }
}
