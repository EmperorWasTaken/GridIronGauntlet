using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private List<Obstacle> obstacles;
    [SerializeField] private int gridSize = 5;
    [SerializeField] private bool _horizontal = true;
    [SerializeField][Range(1f, 7f)] private float minSpawnTime = 2f;
    [SerializeField][Range(1f, 7f)] private float maxSpawnTime = 4f;
    [SerializeField] private float lifeTime = 10f;

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

            var obstacle = Instantiate(
                obstacleToSpawn,
                transform.position,
                transform.localRotation);
            
            var offset = RandomOffset(obstacle);

            switch (direction)
            {
                case Direction.North:
                    obstacle.transform.position -= offset;
                    break;
                case Direction.South:
                    obstacle.transform.position += offset;
                    break;
                case Direction.West:
                    obstacle.transform.position -= offset;
                    break;
                case Direction.East:
                    obstacle.transform.position += offset;
                    break;
            }
            
            StartCoroutine(ObstacleLifeTime(obstacle));
            
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator ObstacleLifeTime(Obstacle obstacle)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(obstacle.gameObject);
    }

    private Vector3 RandomOffset(Obstacle obstacleToSpawn)
    {
        return _horizontal ? 
            new Vector3(Random.Range(0, gridSize - (int)obstacleToSpawn.Size + 1), 0f, 0f) 
            : new Vector3(0f, 0f, Random.Range(0, gridSize - (int)obstacleToSpawn.Size + 1));
    }
}

public enum Direction {
    North,
    East,
    West,
    South
}
