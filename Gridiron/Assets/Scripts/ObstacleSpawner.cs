using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Obstacle> obstacles;
    [SerializeField] private int gridSize = 5;
    [SerializeField] private bool _horizontal = true;
    [SerializeField][Range(1f, 7f)] private float minSpawnTime = 2f;
    [SerializeField][Range(1f, 7f)] private float maxSpawnTime = 4f;

    private Coroutine _spawnCoroutine;
    private GameEventSystem _eventSystem;

    private bool _isPlaying = true;

    private void Awake()
    {
        _eventSystem = GetComponent<GameEventSystem>();
        _spawnCoroutine = StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        if (obstacles.Count == 0)
        {
            _isPlaying = false;
        }
        
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        while (_isPlaying)
        {
            int obstacleIndex = Random.Range(0, obstacles.Count);
            var obstacleToSpawn = obstacles[obstacleIndex];
            
            var offset = _horizontal ? 
                new Vector3(Random.Range(0, gridSize - obstacleToSpawn.Size), 0f, 0f) 
                : new Vector3(0f, 0f, Random.Range(0, gridSize - obstacleToSpawn.Size));
            
            var obstacle = Instantiate(obstacleToSpawn, transform.position + offset, transform.localRotation);
            
            StartCoroutine(ObstacleLifeTime(obstacle));

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    private IEnumerator ObstacleLifeTime(Obstacle obstacle)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obstacle.gameObject);
    }
}
