using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    [Tooltip("Time in seconds between spawns")]
    private float _timeBetweenSpawns;

    //Wave related
    //Informational
    [Tooltip("Whether or not the game is currently in a wave")]
    private bool _inWave = false;
    [Tooltip("Time since the most recent wave started")]
    private float _timeSinceWaveStart;
    [Tooltip("Time since the most recent wave ended")]
    private float _timeSinceWaveEnd;
    [Tooltip("The wait spot given to the previous spawn")]
    private Transform _previousWaitSpot;

    //Configurable
    [Tooltip("The time between spawns while not in waves")]
    [SerializeField] private float _calmSpawnTime = 4;
    [Tooltip("The time between spawns during waves")]
    [SerializeField] private float _waveSpawnTime = 1;
    [Tooltip("The time between waves of enemies")]
    [SerializeField] private float _timeBetweenWaves;
    [Tooltip("How long the spawner will spawn enemies more often")]
    [SerializeField] private float _waveDuration = 5;
    [Tooltip("Whether or not objects will be spawned")]
    [SerializeField] private bool _canSpawn;
    [Tooltip("The prefab for the enemy")]
    [SerializeField] private GameObject _spawn;

    //Variables for the spawn
    [Tooltip("The player, used for setting the target of the enemy's shootbehavior")]
    [SerializeField] private GameObject _player;
    [Tooltip("Possible positions where enemies can waitbefore exiting")]
    [SerializeField] private List<Transform> _waitSpots;
    [Tooltip("Possible positions for enemies to exit the scene")]
    [SerializeField] private List<Transform> _exitSpots;

    // Start is called before the first frame update
    void Start()
    {
        _timeBetweenSpawns = _calmSpawnTime;
        StartCoroutine(SpawnObjects());
    }

    private void Update()
    {
        //If in a wave
        if (_inWave)
        {
            _timeSinceWaveStart += Time.deltaTime;
            //If the time since the wave started is less than the time that waves last
            if (_timeSinceWaveStart >= _waveDuration)
            {
                //Set inWave to be false
                _inWave = false;
                //Reset timeSinceWaveEnd
                _timeSinceWaveEnd = 0;
                //Set the time between spawns to be that of the calm spawn time
                _timeBetweenSpawns = _calmSpawnTime;
            }
        }
        //If not in a wave
        else
        {
            _timeSinceWaveEnd += Time.deltaTime;
            //If the time since the wave ended is less than the time between waves
            if (_timeSinceWaveEnd >= _timeBetweenWaves)
            {
                //Set inWave to be true
                _inWave = true;
                //Reset timeSinceWaveStart
                _timeSinceWaveStart = 0;
                //Set the time between spawns to be that of the wave spawn time
                _timeBetweenSpawns = _waveSpawnTime;
            }
        }
    }

    public IEnumerator SpawnObjects()
    {
        while (_canSpawn)
        {
            //Create a new enemy
            GameObject spawnedEnemy = Instantiate(_spawn, transform.position, new Quaternion());
            spawnedEnemy.GetComponent<EnemyShootingBehaviour>().Target = _player;

            //If only one wait spot exists
            if (_waitSpots.Count == 1)
                //Set the spawn's wait spot to be that one spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().WaitSpot = _waitSpots[0];
            else
            {
                Transform randWaitSpot = _waitSpots[Random.Range(0, _waitSpots.Count)];
                while (randWaitSpot == _previousWaitSpot)
                    randWaitSpot = _waitSpots[Random.Range(0, _waitSpots.Count)];

                //Set a random wait spot that isn't the previous one
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().WaitSpot = randWaitSpot;
                //Set the previousWaitSpot variable to be the new spot
                _previousWaitSpot = randWaitSpot;
            }

            //If only one exit spot exists
            if (_exitSpots.Count == 1)
                //Set the spawn's exit spot to be that one exit spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().ExitSpot = _exitSpots[0];
            else
                //Set a random exit spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().ExitSpot = _exitSpots[Random.Range(0, _exitSpots.Count)];

            //Set the enemy's shoot behavior's target to be the target the spawner was given
            spawnedEnemy.GetComponent<EnemyShootingBehaviour>().Target = _player;

            //Pause before spawning again
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
    }
}
