using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Transform box;
    public float Size { get; private set; }
    public float Speed { get; private set; }

    private GameEventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = GetComponent<GameEventSystem>();
        Size = box.gameObject.transform.localScale.x;
        Speed = 2f;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // PlayerChecking
        // Raise event to reset player
    }
}
