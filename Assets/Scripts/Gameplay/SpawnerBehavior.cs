using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    [Tooltip("The time between spawns while between waves")]
    [SerializeField] private float _calmSpawnTime = 4;
    [Tooltip("The time between spawns during waves")]
    [SerializeField] private float _waveSpawnTime = 1;

    [Tooltip("Whether or not the game is currently in a wave")]
    private bool _inWave = false;
    [Tooltip("Time in seconds between spawns")]
    private float _timeBetweenSpawns;
    [Tooltip("The time between waves of enemies")]
    [SerializeField] private float _timeBetweenWaves;
    [Tooltip("How long the spawner will spawn enemies more often")]
    private float _waveTime = 4;
    [Tooltip("Time since the most recent wave started")]
    private float _timeSinceWaveStart;
    [Tooltip("Time since the most recent wave ended")]
    private float _timeSinceWaveEnd;

    [Tooltip("The object that will be instantiated")]
    [SerializeField] private GameObject _spawn;
    [Tooltip("Whether or not objects will be spawned")]
    [SerializeField] private bool _canSpawn;

    //Variables for the spawn
    [Tooltip("The player, used for setting the target of the enemy's shootbehavior")]
    [SerializeField] private GameObject _player;
    [Tooltip("Possible positions where enemies can sit before exiting")]
    [SerializeField] private Transform[] _sitSpots;
    [Tooltip("Possible positions for enemies to loop around before sitting or exiting")]
    [SerializeField] private Transform[] _loopPositions;
    [Tooltip("Possible positions for enemies to exit the scene")]
    [SerializeField] private Transform[] _exitSpots;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private void Update()
    {
        if (_inWave)
        {
            _timeSinceWaveStart += Time.deltaTime;
            //If the time since the wave started is less than the time that waves last
            if (_timeSinceWaveStart < _waveTime)
                _timeBetweenSpawns = _waveSpawnTime;
            else
            {
                //Set inWave to be false
                _inWave = false;
                _timeSinceWaveEnd = 0;
            }
        }
        else
        {
            _timeSinceWaveEnd += Time.deltaTime;
            //If the time since the wave ended is less than the time between waves
            if (_timeSinceWaveEnd < _timeBetweenWaves)
                _timeBetweenSpawns = _calmSpawnTime;
            else
            {
                //Set inWave to be true
                _inWave = true;
                _timeSinceWaveStart = 0;
            }
        }
    }

    public IEnumerator SpawnObjects()
    {
        while(_canSpawn)
        {
            //Create a new enemy
            GameObject spawnedEnemy = Instantiate(_spawn, transform.position, new Quaternion());

            //Set the enemy's loop positions, sit spot, and exit spots to random ones in the arrays
            spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop1 = _loopPositions[Random.Range(0, _loopPositions.Length - 1)];
            spawnedEnemy.GetComponent<EnemyMovementBehavior>().SitSpot = _sitSpots[Random.Range(0, _sitSpots.Length - 1)];
            spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop2 = _loopPositions[Random.Range(0, _loopPositions.Length - 1)];
            spawnedEnemy.GetComponent<EnemyMovementBehavior>().ExitSpot = _exitSpots[Random.Range(0, _exitSpots.Length - 1)];

            //Set the enemy's shoot behavior's target to be the target the spawner was given


            //Pause before spawning again
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
    }
}
