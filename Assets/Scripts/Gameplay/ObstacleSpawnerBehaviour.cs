using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerBehaviour : MonoBehaviour
{
    [Tooltip("Obstacles to spawn")]
    [SerializeField]
    private GameObject[] _obstacles;
    [Tooltip("The time between obstacles spawning")]
    [SerializeField]
    private float _maxTime;
    private float _timeSinceSpawn;
    private bool _canSpawn = false;

    // Update is called once per frame
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if (_timeSinceSpawn > _maxTime)
            _canSpawn = true;
        else
            _canSpawn = false;

        //grabs a random number based from the amount of item in the array
        int rng = Random.Range(0, _obstacles.Length);
        Vector3 randomPos = new Vector3(Random.Range(-6.5f, 6.5f), transform.position.y, transform.position.z);

        //if the item exists in the array
        if (_obstacles[rng] && _canSpawn)
        {
            Quaternion rotation = new Quaternion(0, 180, 0, 0);
            //create a game object from prefab
            GameObject item = Instantiate(_obstacles[rng], randomPos, rotation);
            _timeSinceSpawn = 0;
        }
    }
}
