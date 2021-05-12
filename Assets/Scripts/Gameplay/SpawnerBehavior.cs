﻿using System.Collections;
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
    private float _waveDuration = 4;
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

            //If only one loop position exists
            if (_loopPositions.Length == 1)
            {
                //Set both loop positions to be the one position
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop1 = _loopPositions[0];
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop2 = _loopPositions[0];
            }
            else
            {
                //Set random loop positions
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop1 = _loopPositions[Random.Range(0, _loopPositions.Length)];
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop2 = _loopPositions[Random.Range(0, _loopPositions.Length)];
            }

            //If only one sit spot exists
            if (_sitSpots.Length == 1)
                //Set the spawn's sit spot to be that one spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().Loop1 = _sitSpots[0];
            else
                //Set a random sit spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().SitSpot = _sitSpots[Random.Range(0, _sitSpots.Length)];

            //If only one exit spot exists
            if (_exitSpots.Length == 1)
                //Set the spawn's exit spot to be that one exit spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().ExitSpot = _exitSpots[0];
            else
                //Set a random exit spot
                spawnedEnemy.GetComponent<EnemyMovementBehavior>().ExitSpot = _exitSpots[Random.Range(0, _exitSpots.Length)];

            //Set the enemy's shoot behavior's target to be the target the spawner was given


            //Pause before spawning again
            yield return new WaitForSeconds(_timeBetweenSpawns);
        }
    }
}
