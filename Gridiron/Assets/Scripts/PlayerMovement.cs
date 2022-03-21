using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void LateUpdate()
    {
        Movement();
    }

    

    private void Movement()
    {
        transform.position +=
            new Vector3(
                _playerInput.horizontal,
                0f,
                _playerInput.vertical).normalized * _moveSpeed * Time.deltaTime;
    }
    
}
