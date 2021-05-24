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
    // Update is called once per frame
    void Update()
    {
        if(_health.Health <= 0)
        {
            //grabs a random number based from the amount of item in the array
            int rng = Random.Range(0, _items.Length);
            //if the item exists in the array
            if (_items[rng])
            {
                //create a game object from prefab
                GameObject item = Instantiate(_items[rng], transform.position, new Quaternion());
                //grab the triple shot script
                TripleShot shot = item.GetComponent<TripleShot>();
                if (shot)//if the script exists
                    shot.Player = gameObject.GetComponent<EnemyShootingBehaviour>().Target;//set the script target to be the enemy target
                Destroy(item, 30);//fall back destroy if the player misses the item
            }
            Destroy(gameObject);//destroy the object that died
        }
    }
}
