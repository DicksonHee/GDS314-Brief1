using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlayer.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement current;

        private Rigidbody _rigidbody;
        private Vector3 _playerInput;
        private Vector3 _forceModifier;

        public float movementSpeed = 5f;
        
        public static Action<float, float> OnApplyForce;

		private void Awake()
		{
			current = this;
		}

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
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void CheckInput()
        {
            _playerInput.x = Input.GetAxisRaw("Horizontal");
            _playerInput.z = Input.GetAxisRaw("Vertical");
        }

        private void ApplyMovement()
        {
            Vector3 inputDir = _playerInput * (movementSpeed * Time.deltaTime);
            Vector3 forceDir = _forceModifier * (movementSpeed * 0.9f * Time.deltaTime);
            _rigidbody.AddForce(inputDir + forceDir, ForceMode.VelocityChange);
            //_rigidbody.MovePosition(_rigidbody.position + inputDir + forceDir);
        }

        private void SetForceModifier(float xVal, float zVal)
        {
            Debug.Log(_forceModifier);
            
            _forceModifier.x = xVal;
            _forceModifier.z = zVal;
            Debug.Log(_forceModifier);
        }

        public void ApplyForce(Vector3 forceDir)
        {
            Vector3 force = forceDir;
            if (_forceModifier.x == 1) force.x = 0;
            if (_forceModifier.z == 1) force.z = 0;
            _rigidbody.AddForce(force, ForceMode.Force);
        }
    }
}

