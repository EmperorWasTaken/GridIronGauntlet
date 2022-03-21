using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput _playerInput;
    private float _moveSpeed = 1;


    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Movement();
    }

    

    private void Movement()
    {
        transform.position +=
            new Vector3(_playerInput.horizontal * _moveSpeed * Time.deltaTime, 0f, _playerInput.vertical * _moveSpeed * Time.deltaTime) ;
    }
    
}
