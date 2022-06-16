using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlayer.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _playerInput;
        private Vector3 _forceModifier;

        public float movementSpeed = 5f;

        public static Action<float, float> OnApplyForce;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerInput = Vector3.zero;
            _forceModifier = Vector3.zero;
        }

        private void OnEnable()
        {
            OnApplyForce += SetForceModifier;
        }

        private void OnDisable()
        {
            OnApplyForce -= SetForceModifier;
        }

        private void Update()
        {
            CheckInput();
            ApplyMovement();
        }

        private void FixedUpdate()
        {
            //
            //ApplyMovement();
        }

        private void CheckInput()
        {
            _playerInput.x = Input.GetAxisRaw("Horizontal");
            _playerInput.z = Input.GetAxisRaw("Vertical");
            _playerInput = _playerInput.normalized;
        }

        //private void ApplyMovement()
        //{
        //    Vector3 inputDir = _playerInput * (movementSpeed * Time.fixedDeltaTime);
        //    Vector3 forceDir = _forceModifier * (movementSpeed * Time.fixedDeltaTime * 0.9f);
        //    _rigidbody.MovePosition(_rigidbody.position + inputDir + forceDir);
        //}

        private void ApplyMovement()
        {
            Vector3 inputDir = _playerInput * (movementSpeed * Time.deltaTime);
            Vector3 forceDir = _forceModifier * (movementSpeed * Time.deltaTime * 0.9f);
            _rigidbody.MovePosition(_rigidbody.position + inputDir + forceDir);
        }

        private void SetForceModifier(float xVal, float zVal)
        {
            _forceModifier.x = xVal;
            _forceModifier.z = zVal;
        }
    }
}

