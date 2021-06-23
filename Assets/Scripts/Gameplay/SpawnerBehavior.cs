using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{

    //Informational
    [Tooltip("Time since the game started")]
    [SerializeField] private float _timeSinceGameStart = 0;
    [Tooltip("Current time in seconds between spawns")]
    [SerializeField] private float _currentTimeBetweenSpawns;
    [Tooltip("Whether or not the game is currently in a wave")]
    [SerializeField] private bool _inWave = false;
    [Tooltip("Time since the most recent wave started")]
    [SerializeField] private float _timeSinceWaveStart;
    [Tooltip("Time since the most recent wave ended")]
    [SerializeField] private float _timeSinceWaveEnd;
    [Tooltip("The wait spot given to the previous spawn")]
    private Transform _previousWaitSpot;

    //Configurable
    [Tooltip("The time between spawns while not in waves")]
    [SerializeField] private float _calmSpawnTime = 4;
    [Tooltip("The time between spawns during waves")]
    [SerializeField] private float _waveSpawnTime = 1;
    [Tooltip("The time between waves of enemies")]
    [SerializeField] private float _timeBetweenWaves = 10;
    [Tooltip("How long the spawner will spawn enemies more often")]
    [SerializeField] private float _waveDuration = 5;
    [Tooltip("Whether or not objects will be spawned")]
    [SerializeField] private bool _canSpawn;
    [Tooltip("The prefab for the enemy")]
    [SerializeField] private GameObject _spawn;
    [Tooltip("The GameManager to pass to enemies")]
    [SerializeField] private GameManagerBehaviour _gameManager;

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
        _currentTimeBetweenSpawns = _calmSpawnTime;
        StartCoroutine(SpawnObjects());
    }

    private void Update()
    {
        _timeSinceGameStart += Time.deltaTime;

        //If in a wave
        if (_inWave)
        {
            _timeSinceWaveStart += Time.deltaTime;
            //If the time since the wave started is greater than or equal to the time that waves last
            if (_timeSinceWaveStart >= _waveDuration)
            {
                //Set inWave to be false
                _inWave = false;
                //Reset timeSinceWaveEnd
                _timeSinceWaveEnd = 0;
                //Set the time between spawns to be the calm spawn time
                _currentTimeBetweenSpawns = _calmSpawnTime;

                //Recalculate the spawn times
                RecalculateSpawnTimes();
            }
        }
        //If not in a wave
        else
        {
            _timeSinceWaveEnd += Time.deltaTime;
            //If the time since the wave ended is greater than or equal to the time between waves
            if (_timeSinceWaveEnd >= _timeBetweenWaves)
            {
                //Set inWave to be true
                _inWave = true;
                //Reset timeSinceWaveStart
                _timeSinceWaveStart = 0;
                //Set the time between spawns to be the wave spawn time
                _currentTimeBetweenSpawns = _waveSpawnTime;

                //Recalculate the spawn times
                RecalculateSpawnTimes();
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
            spawnedEnemy.GetComponent<EnemyDropItemBehaviour>().GameManager = _gameManager;

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
            yield return new WaitForSeconds(_currentTimeBetweenSpawns);
        }
    }

    private void RecalculateSpawnTimes()
    {
        //Decrease the time between spawns during waves and spawns during waves
        _calmSpawnTime /= 1.05f;
        _waveSpawnTime /= 1.025f;

        //Increase the time between waves and the wave duration
        _waveDuration *= 1.05f;
        _timeBetweenWaves *= 1.025f;
    }
}
