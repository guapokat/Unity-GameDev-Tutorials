using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private Transform _player;
    private bool _finishedSpawning;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(_currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        _currentWave = waves[index];

        for (int i = 0; i < _currentWave.count; i++)
        {
            if (_player == null)
            {
                yield break;
            }

            Enemy randomEnemy = _currentWave.enemies[Random.Range(0, _currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == _currentWave.count - 1)
            {
                _finishedSpawning = true;
            }
            else
            {
                _finishedSpawning = false;
            }

            yield return new WaitForSeconds(_currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        // If finished spawning wave and no more enemies
        if (_finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            _finishedSpawning = false;
            if (_currentWaveIndex + 1 < waves.Length)
            {
                _currentWaveIndex++;
                StartCoroutine(StartNextWave(_currentWaveIndex));
            }
            else
            {
                Debug.Log("Game Finished!!!");
            }
        }
    }
}
