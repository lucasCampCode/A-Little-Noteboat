using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _items;
    private HealthBehaviour _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<HealthBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_health.Health <= 0)
        {
            Instantiate(_items[Random.Range(0, _items.Count)], transform.position,new Quaternion());
            Destroy(gameObject);
        }
    }
}
